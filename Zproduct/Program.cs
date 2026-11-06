using Zproduct.Data;

namespace Zproduct
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            try
            {
                Database.Inicializar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo preparar la base de datos:\n" + ex.Message,
                                "Error crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using var login = new loginForm();

            // ShowDialog devuelve OK solo cuando las credenciales y el horario son válidos.
            if (login.ShowDialog() == DialogResult.OK)
            {
                Sesion.UsuarioActual = login.UsuarioAutenticado;
                Sesion.TurnoActual = login.TurnoAutenticado;
                Application.Run(new MainForm());
            }
        }
    }
}