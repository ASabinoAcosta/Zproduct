using System;
using System.Collections.Generic;
using System.Text;

namespace Zproduct.Ventas
{
    public record PagoVenta(string Metodo, decimal Recibido, decimal Cambio);

    // TipoVenta: "contado" o "credito". En crédito no hay pago y el cliente es obligatorio.
    public record DatosVenta(
        int UsuarioId,
        int? TurnoId,
        string TipoVenta,
        int? ClienteId,
        IReadOnlyList<LineaVenta> Lineas,
        PagoVenta? Pago);
}
