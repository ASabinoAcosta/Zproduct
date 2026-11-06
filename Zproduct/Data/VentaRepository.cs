using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Zproduct.Ventas;

namespace Zproduct.Data
{
    public static class VentaRepository
    {
        public static long Confirmar(DatosVenta datos)
        {
            if (datos.Lineas.Count == 0)
                throw new InvalidOperationException("La venta no tiene productos.");

            decimal subtotal = datos.Lineas.Sum(l => l.Importe);
            decimal impuesto = datos.Lineas.Sum(l => l.Impuesto);
            decimal total = subtotal + impuesto;

            bool esCredito = datos.TipoVenta == "credito";

            if (esCredito && datos.ClienteId is null)
                throw new InvalidOperationException("Una venta a crédito requiere un cliente.");

            if (!esCredito)
            {
                if (datos.Pago is null)
                    throw new InvalidOperationException("Falta el pago de la venta al contado.");

                if (datos.Pago.Recibido < total)
                    throw new InvalidOperationException("El monto pagado es menor que el total a pagar.");
            }

            using var con = new NpgsqlConnection(Database.ConnectionString);
            con.Open();
            using var tx = con.BeginTransaction();

            // 1. Número de factura consecutivo (si algo falla más abajo, el número no se pierde)
            long numero = Convert.ToInt64(Escalar(con, tx,
                "UPDATE secuencias SET ultimo_valor = ultimo_valor + 1 WHERE nombre = 'factura' RETURNING ultimo_valor;"));

            // 2. Verificar stock con bloqueo de fila
            foreach (var l in datos.Lineas.Where(l => l.Producto.ControlaInventario))
            {
                int stock = Convert.ToInt32(Escalar(con, tx,
                    "SELECT stock FROM productos WHERE id = @id FOR UPDATE;",
                    ("id", l.Producto.Id)));

                if (stock < l.Cantidad)
                    throw new InvalidOperationException(
                        $"Stock insuficiente de {l.Producto.Nombre}. Disponible: {stock}.");
            }

            // 3. Encabezado de la factura
            long facturaId = Convert.ToInt64(Escalar(con, tx, @"
                INSERT INTO facturas (numero, fecha_hora, usuario_id, turno_id, tipo_venta, cliente_id,
                                      subtotal, impuesto_total, total)
                VALUES (@numero, NOW(), @usuario, @turno, @tipo, @cliente, @subtotal, @impuesto, @total)
                RETURNING id;",
                ("numero", numero), ("usuario", datos.UsuarioId), ("turno", datos.TurnoId),
                ("tipo", datos.TipoVenta), ("cliente", datos.ClienteId),
                ("subtotal", subtotal), ("impuesto", impuesto), ("total", total)));

            // 4. Líneas y movimiento de inventario
            foreach (var l in datos.Lineas)
            {
                Ejecutar(con, tx, @"
                    INSERT INTO factura_lineas (factura_id, producto_id, descripcion, cantidad,
                                                precio_unitario, tasa_impuesto, impuesto, importe)
                    VALUES (@factura, @producto, @descripcion, @cantidad,
                            @precio, @tasa, @impuesto, @importe);",
                    ("factura", facturaId), ("producto", l.Producto.Id),
                    ("descripcion", l.Producto.Nombre), ("cantidad", l.Cantidad),
                    ("precio", l.Producto.Precio), ("tasa", l.Producto.TasaImpuesto),
                    ("impuesto", l.Impuesto), ("importe", l.Importe));

                if (l.Producto.ControlaInventario)
                {
                    Ejecutar(con, tx,
                        "UPDATE productos SET stock = stock - @cantidad WHERE id = @id;",
                        ("cantidad", l.Cantidad), ("id", l.Producto.Id));

                    Ejecutar(con, tx, @"
                        INSERT INTO movimientos_inventario (producto_id, tipo, cantidad, motivo,
                                                            factura_id, usuario_id)
                        VALUES (@producto, 'venta', @cantidad, 'Venta', @factura, @usuario);",
                        ("producto", l.Producto.Id), ("cantidad", -l.Cantidad),
                        ("factura", facturaId), ("usuario", datos.UsuarioId));
                }
            }

            // 5. Al contado se registra el pago; a crédito, la cuenta por cobrar
            if (esCredito)
            {
                Ejecutar(con, tx, @"
                    INSERT INTO cuentas_por_cobrar (factura_id, cliente_id, monto_original, saldo)
                    VALUES (@factura, @cliente, @total, @total);",
                    ("factura", facturaId), ("cliente", datos.ClienteId), ("total", total));
            }
            else
            {
                Ejecutar(con, tx, @"
                    INSERT INTO pagos (factura_id, metodo_pago, importe_recibido, cambio)
                    VALUES (@factura, @metodo, @recibido, @cambio);",
                    ("factura", facturaId), ("metodo", datos.Pago!.Metodo),
                    ("recibido", datos.Pago.Recibido), ("cambio", datos.Pago.Cambio));
            }

            // 6. Auditoría
            Ejecutar(con, tx, @"
                INSERT INTO auditoria (usuario_id, accion, entidad, entidad_id, detalle)
                VALUES (@usuario, 'confirmar_venta', 'factura', @numero, @detalle::jsonb);",
                ("usuario", datos.UsuarioId), ("numero", numero.ToString()),
                ("detalle", JsonSerializer.Serialize(new { total, tipo = datos.TipoVenta, metodo = datos.Pago?.Metodo })));

            tx.Commit();
            return numero;
        }

        private static object? Escalar(NpgsqlConnection con, NpgsqlTransaction tx, string sql,
                                       params (string, object?)[] parametros)
        {
            using var cmd = new NpgsqlCommand(sql, con, tx);

            foreach (var (nombre, valor) in parametros)
                cmd.Parameters.AddWithValue(nombre, valor ?? DBNull.Value);

            return cmd.ExecuteScalar();
        }

        private static void Ejecutar(NpgsqlConnection con, NpgsqlTransaction tx, string sql,
                                     params (string, object?)[] parametros)
        {
            using var cmd = new NpgsqlCommand(sql, con, tx);

            foreach (var (nombre, valor) in parametros)
                cmd.Parameters.AddWithValue(nombre, valor ?? DBNull.Value);

            cmd.ExecuteNonQuery();
        }
    }
}
