using System;

namespace Zproduct.Data
{
    public record Turno(int Id, string Nombre, TimeSpan HoraInicio, TimeSpan HoraFin)
    {
        // Un turno puede cruzar la medianoche (ej. Noche: 18:00 a 06:00).
        public bool EstaVigente(TimeSpan ahora) =>
            HoraInicio <= HoraFin
                ? ahora >= HoraInicio && ahora < HoraFin
                : ahora >= HoraInicio || ahora < HoraFin;

        public string Descripcion =>
            $"{Nombre} ({DateTime.Today.Add(HoraInicio):hh:mm tt} - {DateTime.Today.Add(HoraFin):hh:mm tt})";
    }
}
