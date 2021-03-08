using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Cryptography
{
    public static class AES
    {
        public static string Encrypt(string value, string key)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            var inputBuffer = Encoding.UTF8.GetBytes(value);

            RijndaelManaged rm = new()
            {
                Key = SHA256.HashData(Encoding.UTF8.GetBytes(key)),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform transform = rm.CreateEncryptor();
            var inArray = transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);

            return Convert.ToBase64String(inArray);
        }

        public static string Decrypt(string value, string key)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            var inputBuffer = Convert.FromBase64String(value);

            RijndaelManaged rm = new()
            {
                Key = SHA256.HashData(Encoding.UTF8.GetBytes(key)),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform transform = rm.CreateDecryptor();
            var bytes = transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);

            return Encoding.UTF8.GetString(bytes);
        }
    }
}
