using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Zproduct.Ventas;

namespace Zproduct.Data
{
    public static class FacturaRepository
    {
        public static DataTable Buscar(string texto, int limite = 100)
        {
            bool esNumero = long.TryParse(texto, out long numero);

            const string sql = @"
                SELECT f.numero, f.fecha_hora AS fecha, f.tipo_venta AS tipo,
                       COALESCE(c.nombre, 'Cliente genérico') AS cliente,
                       u.nombre_usuario AS cajero, f.total, f.estado
                FROM facturas f
                JOIN usuarios u ON u.id = f.usuario_id
                LEFT JOIN clientes c ON c.id = f.cliente_id
                WHERE (@numero = 0 OR f.numero = @numero)
                  AND (@texto = '' OR u.nombre_usuario ILIKE @patron OR c.nombre ILIKE @patron)
                ORDER BY f.numero DESC
                LIMIT @limite;";

            var dt = new DataTable();

            using var con = new NpgsqlConnection(Database.ConnectionString);
            using var da = new NpgsqlDataAdapter(sql, con);

            da.SelectCommand!.Parameters.AddWithValue("numero", esNumero ? numero : 0L);
            da.SelectCommand.Parameters.AddWithValue("texto", esNumero ? "" : texto);
            da.SelectCommand.Parameters.AddWithValue("patron", $"%{texto}%");
            da.SelectCommand.Parameters.AddWithValue("limite", limite);
            da.Fill(dt);
            Database.ConvertirALocal(dt, "fecha");

            return dt;
        }

        // Todo lo que necesita la factura impresa: encabezado, pago y líneas.
        public static FacturaDetalle? Obtener(long numero)
        {
            const string sqlEncabezado = @"
                SELECT f.id, f.fecha_hora, f.tipo_venta, u.nombre_completo,
                       c.nombre, c.telefono,
                       f.subtotal, f.impuesto_total, f.total,
                       p.metodo_pago, p.importe_recibido, p.cambio
                FROM facturas f
                JOIN usuarios u ON u.id = f.usuario_id
                LEFT JOIN clientes c ON c.id = f.cliente_id
                LEFT JOIN pagos p ON p.factura_id = f.id
                WHERE f.numero = @numero;";

            const string sqlLineas = @"
                SELECT producto_id, descripcion, cantidad, precio_unitario, impuesto, importe
                FROM factura_lineas
                WHERE factura_id = @factura
                ORDER BY id;";

            using var con = new NpgsqlConnection(Database.ConnectionString);
            con.Open();

            long facturaId;
            DateTime fechaHora;
            string tipoVenta, cajero;
            string? cliente, telefono, metodo;
            decimal subtotal, impuesto, total;
            decimal? recibido, cambio;

            using (var cmd = new NpgsqlCommand(sqlEncabezado, con))
            {
                cmd.Parameters.AddWithValue("numero", numero);
                using var r = cmd.ExecuteReader();

                if (!r.Read())
                {
                    return null;
                }

                facturaId = r.GetInt64(0);
                fechaHora = r.GetDateTime(1).ToLocalTime();
                tipoVenta = r.GetString(2);
                cajero = r.GetString(3);
                cliente = r.IsDBNull(4) ? null : r.GetString(4);
                telefono = r.IsDBNull(5) ? null : r.GetString(5);
                subtotal = r.GetDecimal(6);
                impuesto = r.GetDecimal(7);
                total = r.GetDecimal(8);
                metodo = r.IsDBNull(9) ? null : r.GetString(9);
                recibido = r.IsDBNull(10) ? null : r.GetDecimal(10);
                cambio = r.IsDBNull(11) ? null : r.GetDecimal(11);
            }

            var lineas = new List<LineaFactura>();

            using (var cmd = new NpgsqlCommand(sqlLineas, con))
            {
                cmd.Parameters.AddWithValue("factura", facturaId);
                using var r = cmd.ExecuteReader();

                while (r.Read())
                {
                    lineas.Add(new LineaFactura(
                        r.GetInt32(0), r.GetString(1), r.GetInt32(2),
                        r.GetDecimal(3), r.GetDecimal(4), r.GetDecimal(5)));
                }
            }

            return new FacturaDetalle(numero, fechaHora, tipoVenta, cajero, cliente, telefono,
                                      subtotal, impuesto, total, metodo, recibido, cambio, lineas);
        }
    }
}
