using System;
using System.Collections.Generic;

namespace Zproduct.Ventas
{
    public record LineaFactura(
        int ProductoId,
        string Descripcion,
        int Cantidad,
        decimal Precio,
        decimal Impuesto,
        decimal Importe);

    public record FacturaDetalle(
        long Numero,
        DateTime FechaHora,
        string TipoVenta,
        string Cajero,
        string? Cliente,
        string? Telefono,
        decimal Subtotal,
        decimal Impuesto,
        decimal Total,
        string? MetodoPago,
        decimal? Recibido,
        decimal? Cambio,
        IReadOnlyList<LineaFactura> Lineas);
}
