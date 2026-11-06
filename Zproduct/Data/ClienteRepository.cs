using Npgsql;
using System;
using System.Data;

namespace Zproduct.Data
{
    public static class ClienteRepository
    {
        public static DataTable ObtenerParaCombo(bool incluirGenerico)
        {
            var dt = new DataTable();

            using var con = new NpgsqlConnection(Database.ConnectionString);
            using var da = new NpgsqlDataAdapter(
                "SELECT id, nombre FROM clientes WHERE activo ORDER BY nombre", con);
            da.Fill(dt);

            if (incluirGenerico)
            {
                DataRow generico = dt.NewRow();
                generico["id"] = 0;
                generico["nombre"] = "Cliente genérico";
                dt.Rows.InsertAt(generico, 0);
            }

            return dt;
        }

        public static int Crear(string nombre, string telefono)
        {
            const string sql = @"
                INSERT INTO clientes (nombre, telefono)
                VALUES (@nombre, @telefono)
                RETURNING id;";

            using var con = new NpgsqlConnection(Database.ConnectionString);
            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("nombre", nombre);
            cmd.Parameters.AddWithValue("telefono",
                string.IsNullOrWhiteSpace(telefono) ? DBNull.Value : telefono);

            return (int)cmd.ExecuteScalar()!;
        }
    }
}
