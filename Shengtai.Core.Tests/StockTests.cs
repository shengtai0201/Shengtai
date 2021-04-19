using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Core.Tests
{
    [TestFixture]
    class StockTests
    {
        private (int Buy, bool Result) Buy(double price, int quantity, double discount = 1.0)
        {
            var total = price * quantity;
            var fee = total * 0.001425 * discount;

            var result = (int)Math.Ceiling(total) + (int)Math.Floor(fee);

            return (result, fee > 1);
        }

        private int Sell(double price, int quantity, double discount = 1.0)
        {
            var _total = price * quantity;
            var total = (int)Math.Floor(_total);
            var fee = (int)Math.Floor(_total * 0.001425 * discount);
            var tax = (int)Math.Floor(_total * 0.003);

            return total - fee - tax;
        }


        [Test]
        public void AAA()
        {
            var buy = this.Buy(15.65, 1).Buy;
            buy += this.Buy(15.65, 100).Buy;
            for (double i = 0; i < 0.1; i += 0.01)
            {
                var price = 15.74 + i;
                var sell = this.Sell(price, 101);
                Console.WriteLine($"{buy}, {sell}, {price}, {sell - buy}");
            }
        }
    }
}
