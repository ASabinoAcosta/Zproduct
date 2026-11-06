using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Zproduct.Ventas
{
    public static class ReportePdfGenerator
    {
        private const string NombreNegocio = "Zproduct";
        private const string FuenteBase = "Lato";

        private static readonly Dictionary<string, string> Encabezados = new()
        {
            ["numero"] = "Nº",
            ["fecha"] = "Fecha",
            ["tipo"] = "Tipo",
            ["cliente"] = "Cliente",
            ["cajero"] = "Cajero",
            ["turno"] = "Turno",
            ["bruto"] = "Bruto",
            ["itbis"] = "ITBIS",
            ["total"] = "Total",
            ["contado"] = "Contado",
            ["credito"] = "Crédito",
            ["facturas"] = "Facturas",
            ["concepto"] = "Concepto",
            ["codigo"] = "Código",
            ["producto"] = "Producto",
            ["categoria"] = "Categoría",
            ["stock"] = "Stock",
            ["precio"] = "Precio",
            ["valor"] = "Valor"
        };

        public static string Generar(string titulo, string periodo, string resumen, DataTable datos)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            string rutaCarpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReportesGenerados");
            Directory.CreateDirectory(rutaCarpeta);

            string nombreArchivo = $"{titulo.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(1.5f, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(9).FontFamily(FuenteBase));

                    page.Header().Element(headerContainer => ComposeHeader(headerContainer, titulo, periodo));
                    page.Content().Element(contentContainer => ComposeContent(contentContainer, datos, resumen));
                    page.Footer().Element(footerContainer => ComposeFooter(footerContainer));
                });
            })
            .GeneratePdf(rutaCompleta);

            return rutaCompleta;
        }

        public static void Abrir(string rutaPdf)
        {
            Process.Start(new ProcessStartInfo(rutaPdf) { UseShellExecute = true });
        }

        private static void ComposeHeader(IContainer container, string titulo, string periodo)
        {
            container.Column(column =>
            {
                column.Item().Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text(titulo.ToUpper()).FontSize(18).Bold();
                        col.Item().PaddingTop(3).Text(periodo);
                    });

                    row.ConstantItem(200).Column(col =>
                    {
                        col.Item().AlignRight().Text(NombreNegocio).FontSize(16).Italic();
                        col.Item().AlignRight().Text($"Generado: {DateTime.Now:dd/MM/yyyy hh:mm tt}").FontSize(8);
                    });
                });

                column.Item().PaddingVertical(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
            });
        }

        private static void ComposeContent(IContainer container, DataTable datos, string resumen)
        {
            container.Column(column =>
            {
                if (datos.Rows.Count == 0)
                {
                    column.Item().PaddingVertical(20).Text("No hay datos para este reporte.").Italic();
                    return;
                }

                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        foreach (DataColumn col in datos.Columns)
                        {
                            columns.RelativeColumn(col.DataType == typeof(string) ? 2f : 1f);
                        }
                    });

                    table.Header(header =>
                    {
                        foreach (DataColumn col in datos.Columns)
                        {
                            Alinear(header.Cell().BorderBottom(1).BorderColor(Colors.Black).Background(Colors.Grey.Lighten3), col)
                                .Padding(4)
                                .Text(Encabezado(col.ColumnName))
                                .Bold();
                        }
                    });

                    foreach (DataRow fila in datos.Rows)
                    {
                        foreach (DataColumn col in datos.Columns)
                        {
                            Alinear(table.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2), col)
                                .Padding(4)
                                .Text(Formatear(fila[col], col));
                        }
                    }
                });

                column.Item().PaddingTop(12).AlignRight().Text(resumen).Bold();
            });
        }

        private static void ComposeFooter(IContainer container)
        {
            container.AlignCenter().Text(text =>
            {
                text.Span("Página ").FontSize(8);
                text.CurrentPageNumber().FontSize(8);
                text.Span(" de ").FontSize(8);
                text.TotalPages().FontSize(8);
            });
        }

        private static IContainer Alinear(IContainer celda, DataColumn col)
        {
            bool numerica = col.DataType == typeof(decimal) || col.DataType == typeof(long) || col.DataType == typeof(int);
            return numerica ? celda.AlignRight() : celda.AlignLeft();
        }

        private static string Encabezado(string nombreColumna) =>
            Encabezados.TryGetValue(nombreColumna, out string? texto) ? texto : nombreColumna;

        private static string Formatear(object valor, DataColumn col)
        {
            if (valor is null || valor == DBNull.Value)
            {
                return "-";
            }

            if (col.ColumnName == "numero")
            {
                return Convert.ToInt64(valor).ToString("D5");
            }

            return valor switch
            {
                DateTime fecha => fecha.ToString("dd/MM/yyyy hh:mm tt"),
                decimal monto => monto.ToString("N2"),
                _ => valor.ToString() ?? "-"
            };
        }
    }
}
