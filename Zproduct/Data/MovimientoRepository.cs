using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Zproduct.Data
{
    public static class MovimientoRepository
    {
        public static DataTable ObtenerRecientes(int limite = 50)
        {
            const string sql = @"
                SELECT m.fecha_hora AS fecha, p.nombre AS producto, m.tipo, m.cantidad
                FROM movimientos_inventario m
                JOIN productos p ON p.id = m.producto_id
                ORDER BY m.fecha_hora DESC
                LIMIT @limite;";

            var dt = new DataTable();

            using var con = new NpgsqlConnection(Database.ConnectionString);
            using var da = new NpgsqlDataAdapter(sql, con);
            da.SelectCommand!.Parameters.AddWithValue("limite", limite);
            da.Fill(dt);
            Database.ConvertirALocal(dt, "fecha");

            return dt;
        }
    }
}
