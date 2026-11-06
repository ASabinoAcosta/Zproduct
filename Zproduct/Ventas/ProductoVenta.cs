namespace Zproduct.Ventas
{
    public record ProductoVenta(
        int Id,
        string Codigo,
        string Nombre,
        decimal Precio,
        decimal TasaImpuesto,
        int Stock,
        bool ControlaInventario);
}
