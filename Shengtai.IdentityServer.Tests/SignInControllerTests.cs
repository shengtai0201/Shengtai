using IdentityServer4.Models;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Tests
{
    [TestFixture]
    public class SignInControllerTests
    {
        [Test]
        public void AAA()
        {
            var value = "/O6VOm%p".Sha256();
            var test = System.Text.Encoding.UTF8.GetBytes(value);

            Console.WriteLine(test.ToString());
        }

        [Test]
        public void BBB()
        {
            var x = 7028.66 * 27.65;
            var y = 7000 * 29.78;

            Console.WriteLine($"{x}, {y - x}");
        }

        [Test]
        public void CCC()
        {
            object a = null;
            object b = "test2";

            var test = a ?? b ?? "test3";
            Console.WriteLine(test);
        }

        [Test]
        public void DDD()
        {
            object a = "test1";
            object b = null;

            var test = a ?? b ?? "test3";
            Console.WriteLine(test);
        }

        [Test]
        public void EEE()
        {
            object a = "test1";
            object b = "test2";

            var test = a ?? b ?? "test3";
            Console.WriteLine(test);
        }

        //[Test]
        //public async Task GetExternalAuthenticationSchemes()
        //{
        //    var appsettings = new 

        //    Service.SignInRefService service = new Service.SignInRefService();
        //    await service.GetExternalAuthenticationSchemesAsync();
        //}

        //[Test]
        //public async Task SignOutAsync()
        //{
        //    Service.SignInRefService service = new Service.SignInRefService();
        //    await service.SignOutAsync();
        //}
    }
}
