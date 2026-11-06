using Npgsql;
using Zproduct.Data;

namespace Zproduct
{
    public partial class loginForm : Form
    {
        public Usuario? UsuarioAutenticado { get; private set; }
        public Turno? TurnoAutenticado { get; private set; }

        public loginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            timerRefresco.Interval = 60_000;
            timerRefresco.Start();
            CargarUsuariosDisponibles();
            MostrarBotonAdministrador();
        }

        private void timerRefresco_Tick(object sender, EventArgs e)
        {
            CargarUsuariosDisponibles();
        }

        // Solo existe para crear el primer administrador; los cajeros los crea el administrador.
        private void MostrarBotonAdministrador()
        {
            try
            {
                btnRegistrar.Visible = !UsuarioRepository.ExisteAdministrador();
            }
            catch (NpgsqlException ex)
            {
                btnRegistrar.Visible = false;
                Mostrar("No se pudo verificar el administrador:\n" + ex.Message);
            }
        }

        private void CargarUsuariosDisponibles()
        {
            try
            {
                var turnos = TurnoRepository.ObtenerAsignados();
                TimeSpan ahora = DateTime.Now.TimeOfDay;

                dgvUsuarios.DataSource = UsuarioRepository.ObtenerTodos()
                    .Select(u =>
                    {
                        turnos.TryGetValue(u.Id, out Turno? turno);

                        return new
                        {
                            Usuario = u.NombreUsuario,
                            Nombre = u.NombreCompleto,
                            Rol = u.Rol,
                            Turno = u.Rol == "Administrador" ? "Todo el día" : turno?.Nombre ?? "Sin turno",
                            Estado = ObtenerEstado(u, turno, ahora)
                        };
                    })
                    .ToList();
            }
            catch (NpgsqlException ex)
            {
                Mostrar("No se pudo cargar la lista de usuarios:\n" + ex.Message);
            }
        }

        private static string ObtenerEstado(Usuario usuario, Turno? turno, TimeSpan ahora)
        {
            if (!usuario.Activo) return "Inactivo";
            if (usuario.Rol == "Administrador") return "Disponible";
            if (turno is null) return "Sin turno";

            return turno.EstaVigente(ahora) ? "Disponible" : "Fuera de turno";
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuarioTexto = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text;

            if (string.IsNullOrWhiteSpace(usuarioTexto) || string.IsNullOrEmpty(contrasena))
            {
                Mostrar("Debe escribir el usuario y la contraseña.");
                return;
            }

            try
            {
                Usuario? usuario = UsuarioRepository.ObtenerPorNombre(usuarioTexto);

                if (usuario is null)
                {
                    Mostrar("El usuario no existe.");
                    return;
                }

                if (!usuario.Activo)
                {
                    Mostrar("La cuenta está desactivada.");
                    return;
                }

                if (!PasswordHasher.Verificar(contrasena, usuario.PasswordHash))
                {
                    Mostrar("La contraseña es incorrecta.");
                    return;
                }

                // El cajero solo entra dentro de su turno; el administrador no tiene horario.
                Turno? turno = null;

                if (usuario.Rol == "Cajero")
                {
                    turno = TurnoRepository.ObtenerAsignado(usuario.Id);

                    if (turno is null)
                    {
                        Mostrar("No tiene un turno asignado.\nSolicite al administrador que se lo asigne.");
                        return;
                    }

                    if (!turno.EstaVigente(DateTime.Now.TimeOfDay))
                    {
                        Mostrar($"Su turno {turno.Descripcion} no está vigente.\n" +
                                $"Hora actual: {DateTime.Now:hh:mm tt}.");
                        return;
                    }
                }

                UsuarioAutenticado = usuario;
                TurnoAutenticado = turno;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (FormatException)
            {
                Mostrar("La contraseña almacenada para este usuario no es válida.");
            }
            catch (NpgsqlException ex)
            {
                Mostrar("Error de conexión con la base de datos:\n" + ex.Message);
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string usuarioTexto = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text;

            if (string.IsNullOrWhiteSpace(usuarioTexto) || string.IsNullOrEmpty(contrasena))
            {
                Mostrar("Debe escribir el usuario y la contraseña.");
                return;
            }

            if (contrasena.Length < 6)
            {
                Mostrar("La contraseña debe tener al menos 6 caracteres.");
                return;
            }

            try
            {
                // Se vuelve a comprobar: el botón solo sirve mientras no exista un administrador.
                if (UsuarioRepository.ExisteAdministrador())
                {
                    Mostrar("Ya existe un administrador. Solo él puede crear cuentas de cajero.");
                    btnRegistrar.Visible = false;
                    return;
                }

                var nuevo = new Usuario
                {
                    NombreUsuario = usuarioTexto,
                    NombreCompleto = usuarioTexto,
                    PasswordHash = PasswordHasher.Hash(contrasena),
                    Rol = "Administrador",
                    Activo = true
                };

                UsuarioRepository.Crear(nuevo);

                MessageBox.Show("Administrador creado correctamente.\n" +
                                "Ingrese con esa cuenta para crear los cajeros desde la pestaña Usuarios.",
                    "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtContrasena.Clear();
                btnRegistrar.Visible = false;
                CargarUsuariosDisponibles();
            }
            catch (PostgresException ex) when (ex.SqlState == "23505")
            {
                Mostrar("Ya existe un usuario con ese nombre.");
            }
            catch (NpgsqlException ex)
            {
                Mostrar("Error de conexión con la base de datos:\n" + ex.Message);
            }
        }

        private static void Mostrar(string mensaje)
        {
            MessageBox.Show(mensaje, "Acceso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
