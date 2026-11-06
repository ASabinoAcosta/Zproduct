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

            // 1. ¿Hay configuración guardada y todavía funciona?
            var config = ConfigService.Load();
            var necesitaConfigurar = config is null || !ConfigService.TestConnection(config).ok;

            // 2. Si no, mostramos la pantalla de configuración.
            if (necesitaConfigurar)
            {
                using var setup = new SetupForm();

                // Si cierra la ventana sin guardar, no hay nada que hacer: salimos.
                if (setup.ShowDialog() != DialogResult.OK || setup.Result is null)
                    return;

                config = setup.Result;
            }

            // 3. Dejamos la configuración disponible para toda la app y abrimos el login.
            ConfigService.Current = config!;

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