using System;
using System.Security.Cryptography;

namespace HexKeyGenerator
{
    /// <summary>
    /// Utility class for generating cryptographically secure random 256-bit hex keys
    /// </summary>
    public static class KeyGenerator
    {
        /// <summary>
        /// Generate a random 256-bit (32-byte) key in hexadecimal format
        /// </summary>
        /// <returns>A 64-character hexadecimal string representing a 256-bit key</returns>
        public static string Generate256BitHexKey()
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] keyBytes = new byte[32]; // 256 bits = 32 bytes
                rng.GetBytes(keyBytes);
                return BitConverter.ToString(keyBytes).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// Generate a random 256-bit key and associate it with a company name
        /// </summary>
        /// <param name="companyName">The name of the company</param>
        /// <returns>A tuple containing the company name and the generated hex key</returns>
        public static Tuple<string, string> GenerateKeyForCompany(string companyName)
        {
            if (string.IsNullOrWhiteSpace(companyName))
            {
                throw new ArgumentException("Company name cannot be empty or null.", nameof(companyName));
            }

            string hexKey = Generate256BitHexKey();
            return new Tuple<string, string>(companyName, hexKey);
        }
    }
}
