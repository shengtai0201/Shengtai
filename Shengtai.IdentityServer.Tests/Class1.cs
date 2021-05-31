using IdentityServer4.Models;
using NUnit.Framework;
using System;

namespace Shengtai.IdentityServer.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void AAA()
        {
            var value = "/O6VOm%p".Sha256();
            var test = System.Text.Encoding.UTF8.GetBytes(value);

            Console.WriteLine(test.ToString());
        }
    }
}
