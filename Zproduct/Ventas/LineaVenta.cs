using System;
using System.Collections.Generic;
using System.Text;

namespace Zproduct.Ventas
{
    public class LineaVenta
    {
        public ProductoVenta Producto { get; }
        public int Cantidad { get; set; }

        public LineaVenta(ProductoVenta producto, int cantidad)
        {
            Producto = producto;
            Cantidad = cantidad;
        }

        public decimal Importe => CalculadoraVenta.Importe(Producto.Precio, Cantidad);
        public decimal Impuesto => CalculadoraVenta.Impuesto(Importe, Producto.TasaImpuesto);
    }
}
