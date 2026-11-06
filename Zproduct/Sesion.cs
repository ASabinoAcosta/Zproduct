using System;
using System.Collections.Generic;
using System.Text;
using Zproduct.Data;

namespace Zproduct
{
    public static class Sesion
    {
        public static Usuario? UsuarioActual { get; set; }
        public static Turno? TurnoActual { get; set; }

        public static bool EsAdministrador => UsuarioActual?.Rol == "Administrador";

        // Devuelve null si puede vender; si no, el motivo para mostrarlo en pantalla.
        public static string? MotivoFueraDeTurno()
        {
            if (UsuarioActual is null || EsAdministrador)
            {
                return null;
            }

            if (TurnoActual is null)
            {
                return "No tiene un turno asignado.";
            }

            return TurnoActual.EstaVigente(DateTime.Now.TimeOfDay)
                ? null
                : $"Su turno {TurnoActual.Descripcion} ya terminó. Cierre sesión y vuelva a ingresar en su horario.";
        }
    }
}
