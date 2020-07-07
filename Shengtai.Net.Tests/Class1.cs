using NUnit.Framework;
using Shengtai.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Tests
{
    [TestFixture]
    public class Class1
    {
        static string RSADecrypt(string xmlPrivateKey, string m_strDecryptString)
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString(xmlPrivateKey);
            byte[] rgb = Convert.FromBase64String(m_strDecryptString);
            byte[] bytes = provider.Decrypt(rgb, false);
            return new UnicodeEncoding().GetString(bytes);
        }

        static string RSAEncrypt(string xmlPublicKey, string m_strEncryptString)
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString(xmlPublicKey);
            byte[] bytes = new UnicodeEncoding().GetBytes(m_strEncryptString);
            return Convert.ToBase64String(provider.Encrypt(bytes, false));
        }

        [Test]
        public void BBB()
        {
            // 在personal（个人）里面创建一个foo的证书
            DataCertificate.CreateCertWithPrivateKey("foo", @"C:\Program Files (x86)\Windows Kits\10\bin\10.0.19041.0\x64\makecert.exe");

            // 获取证书
            X509Certificate2 c1 = DataCertificate.GetCertificateFromStore("foo");

            string keyPublic = c1.PublicKey.Key.ToXmlString(false);  // 公钥
            string keyPrivate = c1.PrivateKey.ToXmlString(true);  // 私钥

            string cypher = RSAEncrypt(keyPublic, "程序员");  // 加密
            string plain = RSADecrypt(keyPrivate, cypher);  // 解密

            Assert.AreEqual(plain, "程序员");

            // 生成一个cert文件
            DataCertificate.ExportToCerFile("foo", @"d:\foo.cer");

            X509Certificate2 c2 = DataCertificate.GetCertFromCerFile(@"d:\foo.cer");

            string keyPublic2 = c2.PublicKey.Key.ToXmlString(false);

            bool b = keyPublic2 == keyPublic;
            string cypher2 = RSAEncrypt(keyPublic2, "程序员2");  // 加密
            //string plain2 = RSADecrypt(keyPrivate, cypher2);  // 解密, cer里面并没有私钥，所以这里使用前面得到的私钥来解密

            //Assert.AreEqual(plain2, "程序员2");

            // 生成一个pfx， 并且从store里面删除
            DataCertificate.ExportToPfxFile("foo", @"d:\foo.pfx", "111", true);

            X509Certificate2 c3 = DataCertificate.GetCertificateFromPfxFile(@"d:\foo.pfx", "111");

            string keyPublic3 = c3.PublicKey.Key.ToXmlString(false);  // 公钥
            string keyPrivate3 = c3.PrivateKey.ToXmlString(true);  // 私钥

            string cypher3 = RSAEncrypt(keyPublic3, "程序员3");  // 加密
            string plain3 = RSADecrypt(keyPrivate3, cypher3);  // 解密

            Assert.AreEqual(plain3, "程序员3");
        }

        // ok
        [Test]
        public void AAA()
        {
            var s = "!@#測試123";
            //var certificate = DataCertificate.GetCertificateFromPfxFile(@"D:\Projects\Shengtai\Shengtai.Net.Tests\Signature\1234.pfx", "1234");
            var certificate = new X509Certificate2(@"D:\Projects\Shengtai\Shengtai.Net.Tests\Signature\1234.pfx", "1234", X509KeyStorageFlags.Exportable);

            var publicKey = certificate.PublicKey.Key.ToXmlString(false);
            var privateKey = certificate.PrivateKey.ToXmlString(true);

            //var cypher = RSAEncrypt(publicKey, s);
            var cypher = Rsa.Encrypt(s, publicKey);
            //var plain = RSADecrypt(privateKey, cypher);
            var plain = Rsa.Decrypt(cypher, privateKey);

            Assert.AreEqual(s, plain);
        }
    }
}
