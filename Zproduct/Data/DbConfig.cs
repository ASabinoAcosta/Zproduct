using System;
using System.Collections.Generic;
using System.Text;

namespace Zproduct.Data
{
    // <summary>
    /// Datos de conexión a PostgreSQL. Se guardan cifrados en %AppData%\POS\config.dat
    /// </summary>
    public class DbConfig
    {
        public string Host { get; set; } = "localhost";
        public int Port { get; set; } = 5432;
        public string Username { get; set; } = "postgres";
        public string Password { get; set; } = "";
    }
}
