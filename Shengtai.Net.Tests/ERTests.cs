using NPOI.HSSF.UserModel;
using NUnit.Framework;
using Shengtai.Tests.Exchange;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Tests
{
    [TestFixture]
    public class ERTests
    {
        public ERTests()
        {

        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

        }

        [Test]
        public void AAA()
        {
            IExchange<Rate> rates = new RateCollection(3);

            FileStream s = new FileStream(@"C:\Users\User\Documents\test123.xls", FileMode.Open, FileAccess.Read);
            var workbook = new HSSFWorkbook(s);
            var sheet = workbook.GetSheetAt(0);
            for (int rownum = 0; rownum <= sheet.LastRowNum; rownum++)
            {
                var row = sheet.GetRow(rownum);
                var rate = Rate.NewInstance(row);
                rates.Add(rate);
            }
            workbook.Close();
            s.Close();
            s.Dispose();

            var full = rates.GetFullLevel();
            var lower = rates.GetLowerLevel();
            Console.WriteLine($"最高：{full.Item1}, 滿水位：{full.Item2}, 低水位：{lower.Item1}, 最低：{lower.Item2}");
        }

        private double GetProfit(double sell, double buy, int unit, ref double fraction, ref double denominator, ref double capital)
        {
            fraction += (sell - buy) * unit;
            denominator += buy * unit;
            capital += buy * unit;

            return (sell - buy) * unit;
        }

        [Test]
        public void BBB()
        {
            // 賣紀錄最高(30.16)，買紀錄最低(29.94)
            var full = 30.16;

            double fraction = 0;
            double denominator = 0;
            double capital = 0;
            var profit = this.GetProfit(full, 29.915, 1100, ref fraction, ref denominator, ref capital);
            profit += this.GetProfit(full, 29.925, 900, ref fraction, ref denominator, ref capital);
            profit += this.GetProfit(full, 29.905, 1000, ref fraction, ref denominator, ref capital);
            profit += this.GetProfit(full, 29.875, 1000, ref fraction, ref denominator, ref capital);
            profit += this.GetProfit(full, 29.895, 500, ref fraction, ref denominator, ref capital);
            profit += this.GetProfit(full, 29.885, 500, ref fraction, ref denominator, ref capital);
            profit += this.GetProfit(full, 29.835, 1000, ref fraction, ref denominator, ref capital);
            profit += this.GetProfit(full, 29.795, 1000, ref fraction, ref denominator, ref capital);

            profit += this.GetProfit(full, 29.685, 1000, ref fraction, ref denominator, ref capital);
            profit += this.GetProfit(full, 29.665, 1000, ref fraction, ref denominator, ref capital);

            var rate = Math.Round(fraction / denominator, 6);

            Console.WriteLine($"利潤：{profit}, 投資報酬率：{rate}, 總成本：{capital}");
        }
    }
}
