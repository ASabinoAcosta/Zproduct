BEGIN;

-- USUARIOS Y ACCESO
CREATE TABLE IF NOT EXISTS usuarios (
    id              SERIAL PRIMARY KEY,
    nombre_usuario  VARCHAR(50)  NOT NULL UNIQUE,
    nombre_completo VARCHAR(120) NOT NULL,
    password_hash   TEXT         NOT NULL,
    rol             VARCHAR(20)  NOT NULL CHECK (rol IN ('Administrador', 'Cajero')),
    activo          BOOLEAN      NOT NULL DEFAULT TRUE,
    creado_en       TIMESTAMPTZ  NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS turnos (
    id          SERIAL PRIMARY KEY,
    nombre      VARCHAR(30) NOT NULL UNIQUE,
    hora_inicio TIME        NOT NULL,
    hora_fin    TIME        NOT NULL,
    activo      BOOLEAN     NOT NULL DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS asignaciones_turno (
    id           SERIAL PRIMARY KEY,
    usuario_id   INTEGER NOT NULL REFERENCES usuarios(id),
    turno_id     INTEGER NOT NULL REFERENCES turnos(id),
    fecha_inicio DATE    NOT NULL,
    fecha_fin    DATE,
    CHECK (fecha_fin IS NULL OR fecha_fin >= fecha_inicio)
);

CREATE TABLE IF NOT EXISTS auditoria (
    id          BIGSERIAL PRIMARY KEY,
    usuario_id  INTEGER REFERENCES usuarios(id),
    fecha_hora  TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    accion      VARCHAR(50) NOT NULL,
    entidad     VARCHAR(50) NOT NULL,
    entidad_id  VARCHAR(50),
    detalle     JSONB
);

-- IMPUESTOS (las tasas reales están pendientes, DP-01)
CREATE TABLE IF NOT EXISTS impuestos (
    id             SERIAL PRIMARY KEY,
    nombre         VARCHAR(60)  NOT NULL,
    tasa           NUMERIC(5,2) NOT NULL CHECK (tasa >= 0),
    vigente_desde  DATE         NOT NULL,
    vigente_hasta  DATE,
    simulado       BOOLEAN      NOT NULL DEFAULT FALSE,
    CHECK (vigente_hasta IS NULL OR vigente_hasta >= vigente_desde)
);

INSERT INTO impuestos (nombre, tasa, vigente_desde, simulado)
SELECT 'ITBIS (SIMULADO)', 18.00, CURRENT_DATE, TRUE
WHERE NOT EXISTS (SELECT 1 FROM impuestos WHERE simulado);

-- CATÁLOGO
CREATE TABLE IF NOT EXISTS categorias (
    id      SERIAL PRIMARY KEY,
    nombre  VARCHAR(60) NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS clientes (
    id       SERIAL PRIMARY KEY,
    nombre   VARCHAR(120) NOT NULL,
    telefono VARCHAR(20),
    activo   BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS productos (
    id                  SERIAL PRIMARY KEY,
    codigo              VARCHAR(30)   NOT NULL UNIQUE,
    nombre              VARCHAR(120)  NOT NULL,
    categoria_id        INTEGER REFERENCES categorias(id),
    precio_venta        NUMERIC(12,2) NOT NULL CHECK (precio_venta >= 0),
    impuesto_id         INTEGER REFERENCES impuestos(id),
    controla_inventario BOOLEAN NOT NULL DEFAULT TRUE,
    stock               INTEGER NOT NULL DEFAULT 0 CHECK (stock >= 0),
    activo              BOOLEAN NOT NULL DEFAULT TRUE
);

-- NUMERACIÓN CONSECUTIVA
CREATE TABLE IF NOT EXISTS secuencias (
    nombre        VARCHAR(30) PRIMARY KEY,
    ultimo_valor  BIGINT NOT NULL DEFAULT 0
);

INSERT INTO secuencias (nombre) VALUES ('factura')
ON CONFLICT (nombre) DO NOTHING;

-- VENTAS
CREATE TABLE IF NOT EXISTS facturas (
    id                BIGSERIAL PRIMARY KEY,
    numero            BIGINT NOT NULL UNIQUE,
    fecha_hora        TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    usuario_id        INTEGER NOT NULL REFERENCES usuarios(id),
    turno_id          INTEGER NOT NULL REFERENCES turnos(id),
    tipo_venta        VARCHAR(10) NOT NULL CHECK (tipo_venta IN ('contado', 'credito')),
    cliente_id        INTEGER REFERENCES clientes(id),
    subtotal          NUMERIC(12,2) NOT NULL,
    descuento         NUMERIC(12,2) NOT NULL DEFAULT 0,
    impuesto_total    NUMERIC(12,2) NOT NULL,
    total             NUMERIC(12,2) NOT NULL,
    estado            VARCHAR(10) NOT NULL DEFAULT 'confirmada'
                      CHECK (estado IN ('confirmada', 'anulada')),
    anulada_por       INTEGER REFERENCES usuarios(id),
    fecha_anulacion   TIMESTAMPTZ,
    motivo_anulacion  TEXT,
    CHECK (tipo_venta <> 'credito' OR cliente_id IS NOT NULL),
    CHECK ((estado = 'anulada') = (fecha_anulacion IS NOT NULL))
);

CREATE TABLE IF NOT EXISTS factura_lineas (
    id               BIGSERIAL PRIMARY KEY,
    factura_id       BIGINT NOT NULL REFERENCES facturas(id),
    producto_id      INTEGER NOT NULL REFERENCES productos(id),
    descripcion      VARCHAR(120) NOT NULL,
    cantidad         INTEGER NOT NULL CHECK (cantidad > 0),
    precio_unitario  NUMERIC(12,2) NOT NULL CHECK (precio_unitario >= 0),
    tasa_impuesto    NUMERIC(5,2) NOT NULL DEFAULT 0,
    impuesto         NUMERIC(12,2) NOT NULL DEFAULT 0,
    importe          NUMERIC(12,2) NOT NULL
);

CREATE TABLE IF NOT EXISTS pagos (
    id               BIGSERIAL PRIMARY KEY,
    factura_id       BIGINT NOT NULL UNIQUE REFERENCES facturas(id),
    metodo_pago      VARCHAR(10) NOT NULL DEFAULT 'efectivo'
                     CHECK (metodo_pago IN ('efectivo', 'tarjeta')),
    importe_recibido NUMERIC(12,2) NOT NULL CHECK (importe_recibido >= 0),
    cambio           NUMERIC(12,2) NOT NULL CHECK (cambio >= 0),
    fecha_hora       TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- CRÉDITO (fuera del MVP, pero el esquema ya lo soporta)
CREATE TABLE IF NOT EXISTS cuentas_por_cobrar (
    id              BIGSERIAL PRIMARY KEY,
    factura_id      BIGINT NOT NULL UNIQUE REFERENCES facturas(id),
    cliente_id      INTEGER NOT NULL REFERENCES clientes(id),
    monto_original  NUMERIC(12,2) NOT NULL CHECK (monto_original > 0),
    saldo           NUMERIC(12,2) NOT NULL CHECK (saldo >= 0),
    estado          VARCHAR(10) NOT NULL DEFAULT 'pendiente'
                    CHECK (estado IN ('pendiente', 'pagada', 'anulada')),
    CHECK (saldo <= monto_original)
);

CREATE TABLE IF NOT EXISTS abonos (
    id          BIGSERIAL PRIMARY KEY,
    cuenta_id   BIGINT NOT NULL REFERENCES cuentas_por_cobrar(id),
    importe     NUMERIC(12,2) NOT NULL CHECK (importe > 0),
    fecha_hora  TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    usuario_id  INTEGER NOT NULL REFERENCES usuarios(id)
);

-- MOVIMIENTOS DE INVENTARIO (al final, porque depende de facturas)
CREATE TABLE IF NOT EXISTS movimientos_inventario (
    id           BIGSERIAL PRIMARY KEY,
    producto_id  INTEGER NOT NULL REFERENCES productos(id),
    tipo         VARCHAR(20) NOT NULL
                 CHECK (tipo IN ('venta', 'anulacion_venta', 'entrada', 'salida', 'ajuste')),
    cantidad     INTEGER NOT NULL CHECK (cantidad <> 0),
    motivo       VARCHAR(200),
    factura_id   BIGINT REFERENCES facturas(id),
    usuario_id   INTEGER NOT NULL REFERENCES usuarios(id),
    fecha_hora   TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

ALTER TABLE facturas ALTER COLUMN turno_id DROP NOT NULL;

-- La tabla de movimientos se crea antes que facturas; aquí se enlaza con ella
ALTER TABLE movimientos_inventario ADD COLUMN IF NOT EXISTS factura_id BIGINT REFERENCES facturas(id);

-- TURNOS (horarios configurables; Noche cruza la medianoche)
INSERT INTO turnos (nombre, hora_inicio, hora_fin) VALUES
    ('Mañana', '06:00', '12:00'),
    ('Tarde',  '12:00', '18:00'),
    ('Noche',  '18:00', '06:00')
ON CONFLICT (nombre) DO NOTHING;

COMMIT;
