using System;
using System.Collections.Generic;
using System.Text;

namespace Zproduct.Ventas
{
    public static class CalculadoraVenta
    {
        public static decimal Importe(decimal precioUnitario, int cantidad) =>
            precioUnitario * cantidad;

        public static decimal Impuesto(decimal importe, decimal tasaPorcentaje) =>
            Math.Round(importe * tasaPorcentaje / 100m, 2, MidpointRounding.AwayFromZero);

        public static decimal Cambio(decimal total, decimal recibido) =>
            recibido - total;
    }
}
