using System;
using System.Diagnostics;
using System.IO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Zproduct.Data;

namespace Zproduct.Ventas
{
    public static class FacturaPdfGenerator
    {
        private const string NombreNegocio = "Zproduct";
        private const string FuenteBase = "Lato";

        public static string Generar(long numero)
        {
            // Configurar la licencia de QuestPDF (Comunidad es gratuita)
            QuestPDF.Settings.License = LicenseType.Community;

            // Los datos salen de la base de datos, no de la pantalla: así el PDF siempre coincide con lo guardado
            FacturaDetalle factura = FacturaRepository.Obtener(numero)
                ?? throw new InvalidOperationException($"No existe la factura {numero:D5}.");

            string nombreArchivo = $"factura_{numero:D5}.pdf";
            string rutaCarpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FacturasGeneradas");

            if (!Directory.Exists(rutaCarpeta))
            {
                Directory.CreateDirectory(rutaCarpeta);
            }

            string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10).FontFamily(FuenteBase));

                    page.Header().Element(headerContainer => ComposeHeader(headerContainer, factura));
                    page.Content().Element(contentContainer => ComposeContent(contentContainer, factura));
                    page.Footer().Element(footerContainer => ComposeFooter(footerContainer));
                });
            })
            .GeneratePdf(rutaCompleta);

            return rutaCompleta;
        }

        // Pide a Windows que imprima el PDF; si el visor no lo permite, lo abre para imprimir desde allí.
        public static void Imprimir(string rutaPdf)
        {
            try
            {
                Process.Start(new ProcessStartInfo(rutaPdf)
                {
                    Verb = "print",
                    UseShellExecute = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                });
            }
            catch (System.ComponentModel.Win32Exception)
            {
                Process.Start(new ProcessStartInfo(rutaPdf) { UseShellExecute = true });
            }
        }

        private static string Dinero(decimal valor) => $"RD$ {valor:N2}";

        private static void ComposeHeader(IContainer container, FacturaDetalle factura)
        {
            bool esCredito = factura.TipoVenta == "credito";

            container.Column(column =>
            {
                column.Item().Row(row =>
                {
                    // Título y número de factura (5 dígitos)
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text("FACTURA").FontSize(24).Bold();
                        col.Item().PaddingTop(5).Border(1).BorderColor(Colors.Black).Padding(5)
                            .Text($"Nº: {factura.Numero:D5}").Bold();
                    });

                    row.ConstantItem(120).AlignRight().Text(NombreNegocio).FontSize(20).Italic();
                });

                column.Item().PaddingVertical(15).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                column.Item().Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text("DATOS DE LA FACTURA").Bold().FontSize(9);
                        col.Item().PaddingTop(3).Text($"Fecha: {factura.FechaHora:dd/MM/yyyy}");
                        col.Item().Text($"Hora: {factura.FechaHora:hh:mm:ss tt}");
                        col.Item().Text($"Tipo: {(esCredito ? "Crédito" : "Contado")}");
                        col.Item().Text($"Cajero: {factura.Cajero}");
                    });

                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text("DATOS DEL CLIENTE").Bold().FontSize(9);
                        col.Item().PaddingTop(3).Text(factura.Cliente ?? "Cliente genérico");

                        if (!string.IsNullOrWhiteSpace(factura.Telefono))
                        {
                            col.Item().Text($"Teléfono: {factura.Telefono}");
                        }
                    });
                });
            });
        }

        private static void ComposeContent(IContainer container, FacturaDetalle factura)
        {
            bool esCredito = factura.TipoVenta == "credito";

            container.PaddingVertical(20).Column(column =>
            {
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(35);   // ID producto
                        columns.RelativeColumn(3);    // Nombre producto
                        columns.RelativeColumn(1);    // Cantidad
                        columns.RelativeColumn(1.3f); // Valor
                        columns.RelativeColumn(1.2f); // ITBIS
                        columns.RelativeColumn(1.4f); // Importe
                    });

                    table.Header(header =>
                    {
                        header.Cell().BorderBottom(1).BorderColor(Colors.Black).Padding(5).Text("ID").Bold();
                        header.Cell().BorderBottom(1).BorderColor(Colors.Black).Padding(5).Text("Producto").Bold();
                        header.Cell().BorderBottom(1).BorderColor(Colors.Black).Padding(5).AlignRight().Text("Cantidad").Bold();
                        header.Cell().BorderBottom(1).BorderColor(Colors.Black).Padding(5).AlignRight().Text("Valor").Bold();
                        header.Cell().BorderBottom(1).BorderColor(Colors.Black).Padding(5).AlignRight().Text("ITBIS").Bold();
                        header.Cell().BorderBottom(1).BorderColor(Colors.Black).Padding(5).AlignRight().Text("Importe").Bold();
                    });

                    foreach (LineaFactura linea in factura.Lineas)
                    {
                        table.Cell().Padding(5).Text(linea.ProductoId.ToString());
                        table.Cell().Padding(5).Text(linea.Descripcion);
                        table.Cell().Padding(5).AlignRight().Text(linea.Cantidad.ToString());
                        table.Cell().Padding(5).AlignRight().Text($"{linea.Precio:N2}");
                        table.Cell().Padding(5).AlignRight().Text($"{linea.Impuesto:N2}");
                        table.Cell().Padding(5).AlignRight().Text($"{linea.Importe:N2}");
                    }
                });

                column.Item().PaddingVertical(10).LineHorizontal(0.5f).LineColor(Colors.Grey.Medium);

                // Totales alineados a la derecha
                column.Item().AlignRight().Width(250).Column(col =>
                {
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Text("Total bruto");
                        row.ConstantItem(100).AlignRight().Text(Dinero(factura.Subtotal));
                    });

                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Text("Total ITBIS");
                        row.ConstantItem(100).AlignRight().Text(Dinero(factura.Impuesto));
                    });

                    col.Item().PaddingTop(5).Border(1).BorderColor(Colors.Black).Padding(8).Row(row =>
                    {
                        row.RelativeItem().Text("TOTAL A PAGAR").Bold();
                        row.ConstantItem(100).AlignRight().Text(Dinero(factura.Total)).Bold();
                    });
                });

                // Información de pago (recuadro inferior izquierdo)
                column.Item().PaddingTop(30).Row(row =>
                {
                    row.ConstantItem(240).Border(1).BorderColor(Colors.Black).Padding(10).Column(col =>
                    {
                        col.Item().Text("INFORMACIÓN DE PAGO").Bold().FontSize(9);

                        if (esCredito)
                        {
                            col.Item().PaddingTop(5).Text("Venta a crédito");
                            col.Item().Text("Registrada en cuentas por cobrar");
                            col.Item().Text($"Saldo pendiente: {Dinero(factura.Total)}");
                        }
                        else
                        {
                            string metodo = factura.MetodoPago == "tarjeta" ? "Tarjeta" : "Efectivo";

                            col.Item().PaddingTop(5).Text($"Método: {metodo}");
                            col.Item().Text($"Monto pagado: {Dinero(factura.Recibido ?? factura.Total)}");
                            col.Item().Text($"Devuelta: {Dinero(factura.Cambio ?? 0m)}");
                        }
                    });

                    row.RelativeItem(); // Espacio vacío a la derecha
                });
            });
        }

        private static void ComposeFooter(IContainer container)
        {
            container.AlignCenter().Text("Gracias por su compra").FontSize(9).Bold();
        }
    }
}