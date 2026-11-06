using System.Data;
using Npgsql;

namespace Zproduct.Data
{
    public static class ImpuestoRepository
    {
        public static DataTable ObtenerParaCombo()
        {
            var dt = new DataTable();

            using var con = new NpgsqlConnection(Database.ConnectionString);
            using var da = new NpgsqlDataAdapter(
                "SELECT id, nombre FROM impuestos ORDER BY nombre", con);
            da.Fill(dt);

            return dt;
        }
    }
}
