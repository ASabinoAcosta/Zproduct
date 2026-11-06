using Npgsql;
using System.Data;

namespace Zproduct.Data
{
    public static class UsuarioRepository
    {
        private const string Columnas = "id, nombre_usuario, nombre_completo, password_hash, rol, activo";

        public static List<Usuario> ObtenerTodos()
        {
            var lista = new List<Usuario>();

            using var con = new NpgsqlConnection(Database.ConnectionString);
            con.Open();

            using var cmd = new NpgsqlCommand($"SELECT {Columnas} FROM usuarios ORDER BY nombre_usuario", con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(Leer(reader));
            }

            return lista;
        }

        public static Usuario? ObtenerPorNombre(string nombreUsuario)
        {
            using var con = new NpgsqlConnection(Database.ConnectionString);
            con.Open();

            using var cmd = new NpgsqlCommand(
                $"SELECT {Columnas} FROM usuarios WHERE nombre_usuario = @nombre", con);
            cmd.Parameters.AddWithValue("nombre", nombreUsuario);

            using var reader = cmd.ExecuteReader();
            return reader.Read() ? Leer(reader) : null;
        }

        public static bool ExisteAdministrador()
        {
            using var con = new NpgsqlConnection(Database.ConnectionString);
            con.Open();

            using var cmd = new NpgsqlCommand(
                "SELECT EXISTS (SELECT 1 FROM usuarios WHERE rol = 'Administrador' AND activo)", con);

            return (bool)cmd.ExecuteScalar()!;
        }

        public static void Crear(Usuario usuario)
        {
            using var con = new NpgsqlConnection(Database.ConnectionString);
            con.Open();

            using var cmd = new NpgsqlCommand(@"
                INSERT INTO usuarios (nombre_usuario, nombre_completo, password_hash, rol, activo)
                VALUES (@usuario, @nombreCompleto, @passwordHash, @rol, @activo)", con);

            cmd.Parameters.AddWithValue("usuario", usuario.NombreUsuario);
            cmd.Parameters.AddWithValue("nombreCompleto", usuario.NombreCompleto);
            cmd.Parameters.AddWithValue("passwordHash", usuario.PasswordHash);
            cmd.Parameters.AddWithValue("rol", usuario.Rol);
            cmd.Parameters.AddWithValue("activo", usuario.Activo);

            cmd.ExecuteNonQuery();
        }

        // El cajero y su turno se guardan juntos: si falla uno, no se guarda ninguno.
        public static void CrearCajero(Usuario usuario, int turnoId)
        {
            using var con = new NpgsqlConnection(Database.ConnectionString);
            con.Open();
            using var tx = con.BeginTransaction();

            int usuarioId;

            using (var cmd = new NpgsqlCommand(@"
                INSERT INTO usuarios (nombre_usuario, nombre_completo, password_hash, rol, activo)
                VALUES (@usuario, @nombreCompleto, @passwordHash, 'Cajero', TRUE)
                RETURNING id;", con, tx))
            {
                cmd.Parameters.AddWithValue("usuario", usuario.NombreUsuario);
                cmd.Parameters.AddWithValue("nombreCompleto", usuario.NombreCompleto);
                cmd.Parameters.AddWithValue("passwordHash", usuario.PasswordHash);
                usuarioId = (int)cmd.ExecuteScalar()!;
            }

            InsertarAsignacion(con, tx, usuarioId, turnoId);
            tx.Commit();
        }

        public static void AsignarTurno(int usuarioId, int turnoId)
        {
            using var con = new NpgsqlConnection(Database.ConnectionString);
            con.Open();
            using var tx = con.BeginTransaction();

            // La asignación anterior se cierra ayer; si empezó hoy, se reemplaza.
            using (var cierra = new NpgsqlCommand(@"
                UPDATE asignaciones_turno SET fecha_fin = CURRENT_DATE - 1
                WHERE usuario_id = @usuario AND fecha_fin IS NULL AND fecha_inicio < CURRENT_DATE;
                DELETE FROM asignaciones_turno
                WHERE usuario_id = @usuario AND fecha_fin IS NULL AND fecha_inicio = CURRENT_DATE;", con, tx))
            {
                cierra.Parameters.AddWithValue("usuario", usuarioId);
                cierra.ExecuteNonQuery();
            }

            InsertarAsignacion(con, tx, usuarioId, turnoId);
            tx.Commit();
        }

        public static void CambiarEstado(int usuarioId, bool activo)
        {
            using var con = new NpgsqlConnection(Database.ConnectionString);
            con.Open();

            using var cmd = new NpgsqlCommand("UPDATE usuarios SET activo = @activo WHERE id = @id", con);
            cmd.Parameters.AddWithValue("activo", activo);
            cmd.Parameters.AddWithValue("id", usuarioId);

            cmd.ExecuteNonQuery();
        }

        public static DataTable ObtenerConTurno()
        {
            const string sql = @"
                SELECT u.id, u.nombre_usuario AS usuario, u.nombre_completo AS nombre, u.rol,
                       COALESCE(t.nombre, '-') AS turno,
                       CASE WHEN u.activo THEN 'Activo' ELSE 'Inactivo' END AS estado
                FROM usuarios u
                LEFT JOIN asignaciones_turno a ON a.usuario_id = u.id AND a.fecha_fin IS NULL
                LEFT JOIN turnos t ON t.id = a.turno_id
                ORDER BY u.nombre_usuario;";

            var dt = new DataTable();

            using var con = new NpgsqlConnection(Database.ConnectionString);
            using var da = new NpgsqlDataAdapter(sql, con);
            da.Fill(dt);

            return dt;
        }

        public static DataTable ObtenerCajerosParaCombo(bool incluirTodos)
        {
            var dt = new DataTable();

            using var con = new NpgsqlConnection(Database.ConnectionString);
            using var da = new NpgsqlDataAdapter(
                "SELECT id, nombre_usuario AS nombre FROM usuarios WHERE rol = 'Cajero' ORDER BY nombre_usuario", con);
            da.Fill(dt);

            if (incluirTodos)
            {
                DataRow todos = dt.NewRow();
                todos["id"] = 0;
                todos["nombre"] = "Todos los cajeros";
                dt.Rows.InsertAt(todos, 0);
            }

            return dt;
        }

        private static void InsertarAsignacion(NpgsqlConnection con, NpgsqlTransaction tx, int usuarioId, int turnoId)
        {
            using var cmd = new NpgsqlCommand(@"
                INSERT INTO asignaciones_turno (usuario_id, turno_id, fecha_inicio)
                VALUES (@usuario, @turno, CURRENT_DATE);", con, tx);

            cmd.Parameters.AddWithValue("usuario", usuarioId);
            cmd.Parameters.AddWithValue("turno", turnoId);
            cmd.ExecuteNonQuery();
        }

        private static Usuario Leer(NpgsqlDataReader reader) => new()
        {
            Id = reader.GetInt32(0),
            NombreUsuario = reader.GetString(1),
            NombreCompleto = reader.GetString(2),
            PasswordHash = reader.GetString(3),
            Rol = reader.GetString(4),
            Activo = reader.GetBoolean(5)
        };
    }
}
