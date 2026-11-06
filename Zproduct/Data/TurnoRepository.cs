using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace Zproduct.Data
{
    public static class TurnoRepository
    {
        private const string Consulta = @"
            SELECT a.usuario_id, t.id, t.nombre, t.hora_inicio, t.hora_fin
            FROM asignaciones_turno a
            JOIN turnos t ON t.id = a.turno_id
            WHERE a.fecha_fin IS NULL AND t.activo";

        public static DataTable ObtenerParaCombo()
        {
            var dt = new DataTable();

            using var con = new NpgsqlConnection(Database.ConnectionString);
            using var da = new NpgsqlDataAdapter(
                "SELECT id, nombre FROM turnos WHERE activo ORDER BY hora_inicio", con);
            da.Fill(dt);

            return dt;
        }

        public static Turno? ObtenerAsignado(int usuarioId)
        {
            using var con = new NpgsqlConnection(Database.ConnectionString);
            con.Open();

            using var cmd = new NpgsqlCommand(Consulta + " AND a.usuario_id = @usuario", con);
            cmd.Parameters.AddWithValue("usuario", usuarioId);

            using var reader = cmd.ExecuteReader();
            return reader.Read() ? Leer(reader) : null;
        }

        // Un solo viaje a la base para todos los usuarios (lo usa la lista del login).
        public static Dictionary<int, Turno> ObtenerAsignados()
        {
            var lista = new Dictionary<int, Turno>();

            using var con = new NpgsqlConnection(Database.ConnectionString);
            con.Open();

            using var cmd = new NpgsqlCommand(Consulta, con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista[reader.GetInt32(0)] = Leer(reader);
            }

            return lista;
        }

        private static Turno Leer(NpgsqlDataReader reader) => new(
            reader.GetInt32(1),
            reader.GetString(2),
            reader.GetFieldValue<TimeSpan>(3),
            reader.GetFieldValue<TimeSpan>(4));
    }
}
