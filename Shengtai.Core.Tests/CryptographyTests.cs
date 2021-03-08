using NUnit.Framework;
using Shengtai.Cryptography;
using System;

namespace Shengtai.Core.Tests
{
    [TestFixture]
    public class CryptographyTests
    {
        [Test]
        public void EncryptTest()
        {
            string value = "s0-7+Dcw";
            string key = "tpl77xx";

            var str = AES.Encrypt(value, key);
            Console.WriteLine(str);
        }

        [Test]
        public void DecryptTest()
        {
            string value = "jz9kc7cm628njU3DAgA7YA==";
            string key = "tpl77xx";

            var str = AES.Decrypt(value, key);
            Console.WriteLine(str);
        }
    }
}
