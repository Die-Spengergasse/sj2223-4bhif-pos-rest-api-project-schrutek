using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Helpers
{
    public class HashHelpers
    {
        /// <summary>
        /// Generiert eine Zufallszahl und gibt sie Base64 codiert zurück.
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandom(int length = 128)
        {
            // Salt erzeugen.
            byte[] salt = new byte[length / 8];
            using (System.Security.Cryptography.RandomNumberGenerator rnd =
                System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rnd.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        /// <summary>
        /// Berechnet den HMACSHA256 Wert des Passwortes mit dem übergebenen Salt.
        /// </summary>
        /// <param name="password">Base64 Codiertes Passwort.</param>
        /// <param name="salt">Base64 Codiertes Salt.</param>
        /// <returns></returns>
        public static string CalculateHash(string password, string salt)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(salt))
            {
                throw new ArgumentException("Invalid Salt or Passwort.");
            }
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

            System.Security.Cryptography.HMACSHA256 myHash =
                new System.Security.Cryptography.HMACSHA256(saltBytes);

            byte[] hashedData = myHash.ComputeHash(passwordBytes);

            // Das Bytearray wird als Hexstring zurückgegeben.
            string hashedPassword = Convert.ToBase64String(hashedData);
            return hashedPassword;
        }
    }
}
