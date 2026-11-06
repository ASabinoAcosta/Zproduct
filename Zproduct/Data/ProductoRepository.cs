using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Zproduct.Ventas;

namespace Zproduct.Data
{
    public static class ProductoRepository
    {
        public static DataTable Buscar(string texto, int categoriaId)
        {
            const string sql = @"
                SELECT p.id, p.codigo, p.nombre, c.nombre AS categoria, p.precio_venta AS precio, p.stock
                FROM productos p
                JOIN categorias c ON c.id = p.categoria_id
                WHERE (@categoria = 0 OR p.categoria_id = @categoria)
                  AND (p.nombre ILIKE @texto OR p.codigo ILIKE @texto)
                ORDER BY p.nombre;";

            var dt = new DataTable();

            using var con = new NpgsqlConnection(Database.ConnectionString);
            using var da = new NpgsqlDataAdapter(sql, con);
            da.SelectCommand!.Parameters.AddWithValue("categoria", categoriaId);
            da.SelectCommand.Parameters.AddWithValue("texto", $"%{texto}%");
            da.Fill(dt);

            return dt;
        }

        public static List<ProductoVenta> BuscarParaVenta(string texto)
        {
            const string sql = @"
        SELECT p.id, p.codigo, p.nombre, p.precio_venta,
               COALESCE(i.tasa, 0) AS tasa, p.stock, p.controla_inventario
        FROM productos p
        LEFT JOIN impuestos i ON i.id = p.impuesto_id
        WHERE p.activo AND (p.nombre ILIKE @texto OR p.codigo ILIKE @texto)
        ORDER BY p.nombre
        LIMIT 50;";

            var lista = new List<ProductoVenta>();

            using var con = new NpgsqlConnection(Database.ConnectionString);
            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("texto", $"%{texto}%");
            using var r = cmd.ExecuteReader();

            while (r.Read())
            {
                lista.Add(new ProductoVenta(
                    r.GetInt32(0), r.GetString(1), r.GetString(2),
                    r.GetDecimal(3), r.GetDecimal(4), r.GetInt32(5), r.GetBoolean(6)));
            }

            return lista;
        }
        public static int Crear(string codigo, string nombre, int categoriaId,
                        decimal precio, int? impuestoId, int stock)
        {
            const string sql = @"
        INSERT INTO productos (codigo, nombre, categoria_id, precio_venta, impuesto_id, stock)
        VALUES (@codigo, @nombre, @categoria, @precio, @impuesto, @stock)
        RETURNING id;";

            using var con = new NpgsqlConnection(Database.ConnectionString);
            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("codigo", codigo);
            cmd.Parameters.AddWithValue("nombre", nombre);
            cmd.Parameters.AddWithValue("categoria", categoriaId);
            cmd.Parameters.AddWithValue("precio", precio);
            cmd.Parameters.AddWithValue("impuesto", (object?)impuestoId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("stock", stock);

            return (int)cmd.ExecuteScalar()!;
        }
    }
}
