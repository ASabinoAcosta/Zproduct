using System;
using System.Collections.Generic;
using System.Text;

namespace Zproduct.Ventas
{
    public class CarritoVenta
    {
        private readonly List<LineaVenta> _lineas = new();

        public IReadOnlyList<LineaVenta> Lineas => _lineas;
        public bool EstaVacio => _lineas.Count == 0;

        public int CantidadEnCarrito(int productoId) =>
            _lineas.Where(l => l.Producto.Id == productoId).Sum(l => l.Cantidad);

        public void Agregar(ProductoVenta producto, int cantidad)
        {
            if (cantidad <= 0)
                throw new InvalidOperationException("La cantidad debe ser mayor que cero.");

            int enCarrito = CantidadEnCarrito(producto.Id);

            if (producto.ControlaInventario && enCarrito + cantidad > producto.Stock)
                throw new InvalidOperationException(
                    $"Stock insuficiente de {producto.Nombre}. " +
                    $"Disponible: {producto.Stock - enCarrito}.");

            var existente = _lineas.FirstOrDefault(l => l.Producto.Id == producto.Id);

            if (existente is null)
                _lineas.Add(new LineaVenta(producto, cantidad));
            else
                existente.Cantidad += cantidad;
        }

        public void Limpiar() => _lineas.Clear();

        public (decimal Subtotal, decimal Impuesto, decimal Total) Totales()
        {
            decimal subtotal = _lineas.Sum(l => l.Importe);
            decimal impuesto = _lineas.Sum(l => l.Impuesto);

            return (subtotal, impuesto, subtotal + impuesto);
        }
    }
}
