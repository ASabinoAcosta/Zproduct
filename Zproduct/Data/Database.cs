using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Zproduct.Data
{
    public static class Database
    {
        // Cambia la contraseña por la de tu instalación de PostgreSQL
        public static string Servidor => ConfigService.BuildConnectionString(ConfigService.Current);
        private const string NombreBd = "pos";

        public static string ConnectionString => $"{Servidor};Database={NombreBd}";

        public static void Inicializar()
        {
            CrearBaseSiNoExiste();

            using var con = new NpgsqlConnection(ConnectionString);
            con.Open();

            const string sql = @"
                CREATE TABLE IF NOT EXISTS usuarios (
                    id              SERIAL PRIMARY KEY,
                    nombre_usuario  VARCHAR(50) NOT NULL UNIQUE,
                    nombre_completo VARCHAR(120) NOT NULL DEFAULT '',
                    password_hash   TEXT        NOT NULL,
                    rol             VARCHAR(20) NOT NULL DEFAULT 'Cajero',
                    activo          BOOLEAN     NOT NULL DEFAULT TRUE
                );

                ALTER TABLE usuarios ADD COLUMN IF NOT EXISTS nombre_usuario VARCHAR(50);
                ALTER TABLE usuarios ADD COLUMN IF NOT EXISTS nombre_completo VARCHAR(120) NOT NULL DEFAULT '';
                ALTER TABLE usuarios ADD COLUMN IF NOT EXISTS rol VARCHAR(20) NOT NULL DEFAULT 'Cajero';
                ALTER TABLE usuarios ADD COLUMN IF NOT EXISTS activo BOOLEAN NOT NULL DEFAULT TRUE;
                ALTER TABLE usuarios ADD COLUMN IF NOT EXISTS password_hash TEXT NOT NULL DEFAULT '';
                
                 -- La columna 'nombre' de versiones anteriores ya no se usa; no debe bloquear los inserts
                DO $$
                BEGIN
                    IF EXISTS (
                        SELECT 1
                        FROM information_schema.columns
                        WHERE table_name = 'usuarios' AND column_name = 'nombre') THEN
                        EXECUTE 'ALTER TABLE usuarios ALTER COLUMN nombre DROP NOT NULL';
                    END IF;
                END $$;

                DO $$
                BEGIN
                    IF EXISTS (
                        SELECT 1
                        FROM information_schema.columns
                        WHERE table_name = 'usuarios' AND column_name = 'nombre') THEN
                        EXECUTE 'UPDATE usuarios
                                 SET nombre_usuario = COALESCE(NULLIF(nombre_usuario, ''''), nombre)
                                 WHERE nombre_usuario IS NULL OR nombre_usuario = ''''';
                    END IF;
                END $$;

                UPDATE usuarios
                SET nombre_completo = COALESCE(NULLIF(nombre_completo, ''), nombre_usuario)
                WHERE nombre_completo = '';

                CREATE TABLE IF NOT EXISTS categorias (
                    id      SERIAL PRIMARY KEY,
                    nombre  VARCHAR(60) NOT NULL UNIQUE
                );

                CREATE TABLE IF NOT EXISTS impuestos (
                    id             SERIAL PRIMARY KEY,
                    nombre         VARCHAR(60) NOT NULL,
                    tasa           NUMERIC(5,2) NOT NULL CHECK (tasa >= 0),
                    vigente_desde  DATE NOT NULL DEFAULT CURRENT_DATE,
                    vigente_hasta  DATE,
                    simulado       BOOLEAN NOT NULL DEFAULT FALSE,
                    CHECK (vigente_hasta IS NULL OR vigente_hasta >= vigente_desde)
                );

                CREATE TABLE IF NOT EXISTS productos (
                    id                  SERIAL PRIMARY KEY,
                    codigo              VARCHAR(30) NOT NULL UNIQUE,
                    nombre              VARCHAR(120) NOT NULL,
                    categoria_id        INTEGER NOT NULL REFERENCES categorias(id),
                    precio_venta        NUMERIC(12,2) NOT NULL CHECK (precio_venta >= 0),
                    impuesto_id         INTEGER REFERENCES impuestos(id),
                    controla_inventario BOOLEAN NOT NULL DEFAULT TRUE,
                    stock               INTEGER NOT NULL DEFAULT 0 CHECK (stock >= 0),
                    activo              BOOLEAN NOT NULL DEFAULT TRUE
                );

                CREATE TABLE IF NOT EXISTS movimientos_inventario (
                    id          BIGSERIAL PRIMARY KEY,
                    producto_id INTEGER NOT NULL REFERENCES productos(id),
                    tipo        VARCHAR(20) NOT NULL,
                    cantidad    INTEGER NOT NULL CHECK (cantidad <> 0),
                    motivo      VARCHAR(200),
                    usuario_id  INTEGER NOT NULL REFERENCES usuarios(id),
                    fecha_hora  TIMESTAMPTZ NOT NULL DEFAULT NOW()
                );

                INSERT INTO categorias (nombre)
                VALUES ('General')
                ON CONFLICT (nombre) DO NOTHING;

                INSERT INTO impuestos (nombre, tasa, simulado)
                SELECT 'ITBIS (SIMULADO)', 18.00, TRUE
                WHERE NOT EXISTS (SELECT 1 FROM impuestos WHERE simulado);";

            using var cmd = new NpgsqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            // Facturas, pagos, cuentas por cobrar, turnos y numeración
            EjecutarEsquema(con);
        }

        private static void EjecutarEsquema(NpgsqlConnection con)
        {
            string ruta = Path.Combine(AppContext.BaseDirectory, "Data", "schema.sql");

            // El BEGIN/COMMIT del archivo se reemplaza por una transacción de C#
            string sql = File.ReadAllText(ruta).Replace("BEGIN;", "").Replace("COMMIT;", "");

            using var tx = con.BeginTransaction();
            using var cmd = new NpgsqlCommand(sql, con, tx);
            cmd.ExecuteNonQuery();
            tx.Commit();
        }

        // PostgreSQL entrega las fechas en UTC; esto las pasa a la hora de la máquina para mostrarlas.
        public static void ConvertirALocal(System.Data.DataTable tabla, params string[] columnas)
        {
            foreach (string nombre in columnas)
            {
                if (!tabla.Columns.Contains(nombre))
                {
                    continue;
                }

                foreach (System.Data.DataRow fila in tabla.Rows)
                {
                    if (fila[nombre] is DateTime utc)
                    {
                        fila[nombre] = utc.ToLocalTime();
                    }
                }
            }
        }

        private static void CrearBaseSiNoExiste()
        {
            // No podemos crear una base conectándonos a ella misma, así que usamos "postgres"
            using var con = new NpgsqlConnection($"{Servidor};Database=postgres");
            con.Open();

            using var existe = new NpgsqlCommand("SELECT 1 FROM pg_database WHERE datname = @nombre", con);
            existe.Parameters.AddWithValue("nombre", NombreBd);

            if (existe.ExecuteScalar() == null)
            {
                using var crear = new NpgsqlCommand($"CREATE DATABASE \"{NombreBd}\"", con);
                crear.ExecuteNonQuery();
            }
        }
    }
}
