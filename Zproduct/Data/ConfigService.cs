using Npgsql;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Zproduct.Data;

public static class ConfigService
{
    // CAMBIA ESTO por el nombre real de tu base de datos (no es un dato secreto).
    public const string DatabaseName = "pos";

    private static readonly string Folder = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "POS");

    private static readonly string FilePath = Path.Combine(Folder, "config.dat");

    /// <summary>Configuración activa durante la ejecución del programa.</summary>
    public static DbConfig Current { get; set; } = new();

    /// <summary>
    /// Devuelve la configuración guardada, o null si no existe o no se puede leer
    /// (por ejemplo, si el archivo fue copiado desde otra cuenta de Windows).
    /// </summary>
    public static DbConfig? Load()
    {
        try
        {
            if (!File.Exists(FilePath)) return null;

            var cifrado = File.ReadAllBytes(FilePath);
            var bytes = ProtectedData.Unprotect(cifrado, null, DataProtectionScope.CurrentUser);
            return JsonSerializer.Deserialize<DbConfig>(Encoding.UTF8.GetString(bytes));
        }
        catch
        {
            return null;
        }
    }

    /// <summary>Guarda la configuración cifrada con la cuenta de Windows del usuario.</summary>
    public static void Save(DbConfig config)
    {
        Directory.CreateDirectory(Folder);

        var json = JsonSerializer.Serialize(config);
        var cifrado = ProtectedData.Protect(
            Encoding.UTF8.GetBytes(json), null, DataProtectionScope.CurrentUser);

        File.WriteAllBytes(FilePath, cifrado);
    }

    /// <summary>
    /// Construye la cadena de conexión. Si no se indica base de datos, usa la del POS.
    /// </summary>
    public static string BuildConnectionString(DbConfig config, string? database = null)
    {
        return new NpgsqlConnectionStringBuilder
        {
            Host = config.Host,
            Port = config.Port,
            Username = config.Username,
            Password = config.Password,
            Database = database ?? DatabaseName
        }.ToString();
    }

    /// <summary>
    /// Prueba la conexión al servidor usando la base "postgres", que siempre existe.
    /// Así funciona incluso antes de crear la base de datos del POS.
    /// </summary>
    public static (bool ok, string mensaje) TestConnection(DbConfig config)
    {
        try
        {
            using var conn = new NpgsqlConnection(BuildConnectionString(config, "postgres"));
            conn.Open();
            return (true, "");
        }
        // PostgresException hereda de NpgsqlException, por eso va primero.
        catch (PostgresException ex) when (ex.SqlState is "28P01" or "28000")
        {
            return (false, "Usuario o contraseña incorrectos.");
        }
        catch (NpgsqlException)
        {
            return (false, "No se pudo conectar. Verifica que PostgreSQL esté instalado, " +
                           "encendido y que el servidor y el puerto sean correctos.");
        }
        catch (Exception ex)
        {
            return (false, "Error inesperado: " + ex.Message);
        }
    }
}

