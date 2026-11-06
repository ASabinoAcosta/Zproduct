using System;
using System.Collections.Generic;
using System.Text;

namespace Zproduct.Data
{
    public class Usuario
    {
            public int Id { get; set; }
            public string NombreUsuario { get; set; } = "";
            public string NombreCompleto { get; set; } = "";
            public string PasswordHash { get; set; } = "";
            public string Rol { get; set; } = "";
            public bool Activo { get; set; }
    }
}
