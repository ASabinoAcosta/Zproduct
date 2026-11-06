using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Zproduct.Data
{
    public static class CategoriaRepository
    {
        public static DataTable ObtenerParaCombo(bool incluirTodas)
        {
            var dt = new DataTable();

            using var con = new NpgsqlConnection(Database.ConnectionString);
            using var da = new NpgsqlDataAdapter("SELECT id, nombre FROM categorias ORDER BY nombre", con);
            da.Fill(dt);

            if (incluirTodas)
            {
                DataRow todas = dt.NewRow();
                todas["id"] = 0;
                todas["nombre"] = "Todas las categorías";
                dt.Rows.InsertAt(todas, 0);
            }

            return dt;
        }
    }
}
