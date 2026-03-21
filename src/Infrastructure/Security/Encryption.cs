using System;
using System.Security.Cryptography;
using System.Text;
using Domain.Security.Interfaces;

namespace Infrastructure.Security
{
    public class Encryption : IEncryption
    {   
        public string GenerateSalt(int size = 32)
        {
            var saltBytes = new byte[size];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }
        
        public string GenerateHash(string plainText, string salt)
        {
            using (SHA384 sha384Hash = SHA384.Create())
            {
                var saltedPlainText = plainText + salt;
                byte[] bytes = sha384Hash.ComputeHash(Encoding.UTF8.GetBytes(saltedPlainText));
                
                var stringBuilder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    stringBuilder.Append(bytes[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
        public bool VerifyHash(string plainText, string salt, string hash)
        {
            var newHash = GenerateHash(plainText, salt);
            return newHash.Equals(hash, StringComparison.OrdinalIgnoreCase);
        }
    }
}