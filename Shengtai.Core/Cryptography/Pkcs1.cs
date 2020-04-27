namespace Shengtai.Cryptography
{
    public class Pkcs1 : Pkcs7
    {
        public string Signature { get; set; }
        public string Certificate { get; set; }
    }
}