using System;
using System.Security.Cryptography;

namespace Zproduct.Data
{
    public static class PasswordHasher
    {
        private const int TamanoSal = 16;
        private const int TamanoHash = 32;
        private const int Iteraciones = 100_000;
        private static readonly HashAlgorithmName Algoritmo = HashAlgorithmName.SHA256;

        
        public static string Hash(string contrasena)
        {
            byte[] sal = RandomNumberGenerator.GetBytes(TamanoSal);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(contrasena, sal, Iteraciones, Algoritmo, TamanoHash);

            return $"{Iteraciones}.{Convert.ToBase64String(sal)}.{Convert.ToBase64String(hash)}";
        }

        public static bool Verificar(string contrasena, string almacenado)
        {
            string[] partes = almacenado.Split('.');
            if (partes.Length != 3) return false;

            if (!int.TryParse(partes[0], out int iteraciones)) return false;

            byte[] sal = Convert.FromBase64String(partes[1]);
            byte[] hashGuardado = Convert.FromBase64String(partes[2]);

            byte[] hashNuevo = Rfc2898DeriveBytes.Pbkdf2(contrasena, sal, iteraciones, Algoritmo, hashGuardado.Length);

            return CryptographicOperations.FixedTimeEquals(hashNuevo, hashGuardado);
        }
    }
}
