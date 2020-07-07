﻿using System;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;

namespace Shengtai.Cryptography
{
    public class Rsa
    {
        private readonly object halg = null;
        private readonly CspParameters parameters;
        private RSACryptoServiceProvider provider = null;

        public Rsa(string keyContainerName)
        {
            this.parameters = new CspParameters(1024)
            {
                KeyContainerName = keyContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore,
                ProviderName = "Microsoft Strong Cryptographic Provider",
                ProviderType = 1,
                CryptoKeySecurity = new CryptoKeySecurity()
            };
            this.parameters.CryptoKeySecurity.SetAccessRule(
                new CryptoKeyAccessRule("Everyone", CryptoKeyRights.FullControl, AccessControlType.Allow));

            this.halg = new SHA1CryptoServiceProvider();
            this.SetProvider();
        }

        private void SetProvider(bool regenerate = true)
        {
            if (regenerate)
            {
                if (this.provider != null)
                {
                    this.provider.PersistKeyInCsp = false;
                    this.provider.Clear();
                }

                this.provider = new RSACryptoServiceProvider(this.parameters);
            }
        }

        public void GenerateKey(Action<string> setPublicKey, Action<string> setPrivateKey, bool regenerate = true)
        {
            this.SetProvider(regenerate);

            setPublicKey(Convert.ToBase64String(Encoding.UTF8.GetBytes(this.provider.ToXmlString(false))));
            setPrivateKey(Convert.ToBase64String(Encoding.UTF8.GetBytes(this.provider.ToXmlString(true))));
        }

        public void GenerateKey(Action<byte[]> setPublicKey, Action<byte[]> setPrivateKey, bool regenerate = true)
        {
            this.SetProvider(regenerate);

            setPublicKey(Encoding.UTF8.GetBytes(this.provider.ToXmlString(false)));
            setPrivateKey(Encoding.UTF8.GetBytes(this.provider.ToXmlString(true)));
        }

        //public string Encrypt(string s, string publicKey)
        //{
        //    this.SetProvider(this.provider == null);
        //    this.provider.FromXmlString(Encoding.UTF8.GetString(Convert.FromBase64String(publicKey)));

        //    return Convert.ToBase64String(this.provider.Encrypt(Encoding.UTF8.GetBytes(s), false));
        //}

        public static string Encrypt(string s, string publicKey)
        {
            var provider = new RSACryptoServiceProvider();
            provider.FromXmlString(publicKey);
            byte[] rgb = Encoding.UTF8.GetBytes(s);

            var inArray = provider.Encrypt(rgb, false);
            return Convert.ToBase64String(inArray);
        }

        //public string Decrypt(string s, string privateKey)
        //{
        //    this.SetProvider(this.provider == null);
        //    this.provider.FromXmlString(Encoding.UTF8.GetString(Convert.FromBase64String(privateKey)));

        //    return Encoding.UTF8.GetString(this.provider.Decrypt(Convert.FromBase64String(s), false));
        //}

        public static string Decrypt(string s, string privateKey)
        {
            var provider = new RSACryptoServiceProvider();
            provider.FromXmlString(privateKey);
            byte[] rgb = Convert.FromBase64String(s);

            byte[] bytes = provider.Decrypt(rgb, false);
            return Encoding.UTF8.GetString(bytes);
        }

        public string SignData(string s, string privateKey)
        {
            this.SetProvider(this.provider == null);
            this.provider.FromXmlString(Encoding.UTF8.GetString(Convert.FromBase64String(privateKey)));

            return Convert.ToBase64String(this.provider.SignData(Encoding.UTF8.GetBytes(s), this.halg));
        }

        public byte[] SignData(byte[] buffer, byte[] privateKey)
        {
            this.SetProvider(this.provider == null);
            this.provider.FromXmlString(Encoding.UTF8.GetString(privateKey));

            return this.provider.SignData(buffer, this.halg);
        }

        //public bool VerifyData(string s, string privateKey, string publicKey, string signature, out string data)
        //{
        //    this.SetProvider(this.provider == null);
        //    this.provider.FromXmlString(Encoding.UTF8.GetString(Convert.FromBase64String(publicKey)));

        //    data = this.Decrypt(s, privateKey);
        //    return this.provider.VerifyData(Encoding.UTF8.GetBytes(data), this.halg,
        //        Convert.FromBase64String(signature));
        //}

        public bool VerifyData(byte[] buffer, byte[] publicKey, byte[] signature)
        {
            this.SetProvider(this.provider == null);
            this.provider.FromXmlString(Encoding.UTF8.GetString(publicKey));

            return this.provider.VerifyData(buffer, this.halg, signature);
        }
    }
}