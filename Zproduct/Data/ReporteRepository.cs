using Npgsql;
using System;
using System.Data;

namespace Zproduct.Data
{
    public static class ReporteRepository
    {
        public static DataTable CuadreDelDia(DateTime fecha, int cajeroId)
        {
            const string sql = @"
                SELECT CASE WHEN f.tipo_venta = 'credito' THEN 'Crédito'
                            WHEN p.metodo_pago = 'tarjeta' THEN 'Contado - Tarjeta'
                            ELSE 'Contado - Efectivo' END AS concepto,
                       COUNT(*) AS facturas,
                       SUM(f.subtotal) AS bruto,
                       SUM(f.impuesto_total) AS itbis,
                       SUM(f.total) AS total
                FROM facturas f
                LEFT JOIN pagos p ON p.factura_id = f.id
                WHERE f.estado = 'confirmada'
                  AND f.fecha_hora >= @desde AND f.fecha_hora < @hasta
                  AND (@cajero = 0 OR f.usuario_id = @cajero)
                GROUP BY 1
                ORDER BY 1;";

            var (desde, hasta) = Rango(fecha, fecha);
            return Consultar(sql, ("desde", desde), ("hasta", hasta), ("cajero", cajeroId));
        }

        public static DataTable PorCajero(DateTime desde, DateTime hasta, int cajeroId)
        {
            const string sql = @"
                SELECT u.nombre_usuario AS cajero,
                       COALESCE(t.nombre, '-') AS turno,
                       COUNT(*) AS facturas,
                       SUM(CASE WHEN f.tipo_venta = 'contado' THEN f.total ELSE 0 END) AS contado,
                       SUM(CASE WHEN f.tipo_venta = 'credito' THEN f.total ELSE 0 END) AS credito,
                       SUM(f.subtotal) AS bruto,
                       SUM(f.impuesto_total) AS itbis,
                       SUM(f.total) AS total
                FROM facturas f
                JOIN usuarios u ON u.id = f.usuario_id
                LEFT JOIN turnos t ON t.id = f.turno_id
                WHERE f.estado = 'confirmada'
                  AND f.fecha_hora >= @desde AND f.fecha_hora < @hasta
                  AND (@cajero = 0 OR f.usuario_id = @cajero)
                GROUP BY u.nombre_usuario, t.nombre
                ORDER BY u.nombre_usuario, t.nombre;";

            var (inicio, fin) = Rango(desde, hasta);
            return Consultar(sql, ("desde", inicio), ("hasta", fin), ("cajero", cajeroId));
        }

        public static DataTable Inventario()
        {
            const string sql = @"
                SELECT p.codigo, p.nombre AS producto,
                       COALESCE(c.nombre, '-') AS categoria,
                       p.stock, p.precio_venta AS precio,
                       p.stock * p.precio_venta AS valor
                FROM productos p
                LEFT JOIN categorias c ON c.id = p.categoria_id
                WHERE p.activo AND p.controla_inventario
                ORDER BY p.nombre;";

            return Consultar(sql);
        }

        // tipo: "contado", "credito" o "" para ambos
        public static DataTable Facturas(DateTime desde, DateTime hasta, int cajeroId, string tipo)
        {
            const string sql = @"
                SELECT f.numero, f.fecha_hora AS fecha, f.tipo_venta AS tipo,
                       COALESCE(c.nombre, 'Cliente genérico') AS cliente,
                       u.nombre_usuario AS cajero,
                       f.subtotal AS bruto, f.impuesto_total AS itbis, f.total
                FROM facturas f
                JOIN usuarios u ON u.id = f.usuario_id
                LEFT JOIN clientes c ON c.id = f.cliente_id
                WHERE f.estado = 'confirmada'
                  AND f.fecha_hora >= @desde AND f.fecha_hora < @hasta
                  AND (@cajero = 0 OR f.usuario_id = @cajero)
                  AND (@tipo = '' OR f.tipo_venta = @tipo)
                ORDER BY f.numero;";

            var (inicio, fin) = Rango(desde, hasta);
            return Consultar(sql, ("desde", inicio), ("hasta", fin), ("cajero", cajeroId), ("tipo", tipo));
        }

        public static DataTable TopClientes(DateTime desde, DateTime hasta)
        {
            const string sql = @"
                SELECT c.nombre AS cliente,
                       COUNT(*) AS facturas,
                       SUM(f.total) AS total
                FROM facturas f
                JOIN clientes c ON c.id = f.cliente_id
                WHERE f.estado = 'confirmada'
                  AND f.fecha_hora >= @desde AND f.fecha_hora < @hasta
                GROUP BY c.id, c.nombre
                ORDER BY SUM(f.total) DESC
                LIMIT 5;";

            var (inicio, fin) = Rango(desde, hasta);
            return Consultar(sql, ("desde", inicio), ("hasta", fin));
        }

        // Los días se cuentan con la hora de la máquina; la base de datos guarda UTC.
        private static (DateTime Inicio, DateTime Fin) Rango(DateTime desde, DateTime hasta)
        {
            DateTime inicio = DateTime.SpecifyKind(desde.Date, DateTimeKind.Local).ToUniversalTime();
            DateTime fin = DateTime.SpecifyKind(hasta.Date.AddDays(1), DateTimeKind.Local).ToUniversalTime();

            return (inicio, fin);
        }

        private static DataTable Consultar(string sql, params (string Nombre, object Valor)[] parametros)
        {
            var dt = new DataTable();

            using var con = new NpgsqlConnection(Database.ConnectionString);
            using var da = new NpgsqlDataAdapter(sql, con);

            foreach (var (nombre, valor) in parametros)
            {
                da.SelectCommand!.Parameters.AddWithValue(nombre, valor);
            }

            da.Fill(dt);
            Database.ConvertirALocal(dt, "fecha");

            return dt;
        }
    }
}
