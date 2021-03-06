﻿using NUnit.Framework;
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
            var buy = this.Buy(15.68, 1).Buy;
            buy += this.Buy(15.68, 100).Buy;
            buy += this.Buy(16.68, 100).Buy;
            buy += this.Buy(16.6, 100).Buy;
            buy += this.Buy(15.65, 100).Buy;
            buy += this.Buy(15.23, 200).Buy;
            buy += this.Buy(14.7, 200).Buy;
            buy += this.Buy(14.4, 100).Buy;
            buy += this.Buy(13.91, 100).Buy;
            buy += this.Buy(15.14, 100).Buy;

            var sell = this.Sell(15.36, 1101);   // 15.36
            Console.WriteLine($"{buy}, {sell}, {sell - buy}");
        }

        [Test]
        public void BBB()
        {
            var buy = this.Buy(581.00, 5).Buy;

            var sell = this.Sell(595.00, 5);   // 585
            Console.WriteLine($"{buy}, {sell}, {sell - buy}");
        }

        private DateTime GetDateTime(string value)
        {
            var values = value.Split(new[] { "/", "年", "月", "日" }, StringSplitOptions.RemoveEmptyEntries);
            var year = Convert.ToInt32(values[0]) + 1911;
            var month = Convert.ToInt32(values[1]);
            var day = Convert.ToInt32(values[2]);

            return new DateTime(year, month, day);
        }

        //[Test]
        //public void BBB()
        //{
        //    using var reader = new StreamReader(@"D:\OneDrive\文件\ETF.csv");
        //    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        //    var dbContext = new NarcissusDbContext();

        //    csv.Read();
        //    csv.ReadHeader();
        //    while (csv.Read())
        //    {
        //        var symbol = csv.GetField<string>(0);
        //        var name = csv.GetField<string>(1);

        //        var exDividendDate = this.GetDateTime(csv.GetField<string>(2));
        //        var recordDate = this.GetDateTime(csv.GetField<string>(3));
        //        var paymentDay = this.GetDateTime(csv.GetField<string>(4));
        //        var amount = csv.GetField<decimal>(5);
        //        var year = csv.GetField<short>(7);

        //        var stock = dbContext.Stocks.SingleOrDefault(x => x.Symbol == symbol);
        //        if (stock == null)
        //        {
        //            stock = new Stock
        //            {
        //                Symbol = symbol,
        //                Name = name
        //            };
        //            dbContext.Stocks.Add(stock);
        //        }

        //        var etf = dbContext.Etfs.SingleOrDefault(x => x.StockSymbol == symbol && x.ExDividendDate == exDividendDate && x.RecordDate == recordDate && x.PaymentDay == paymentDay);
        //        if (etf == null)
        //        {
        //            stock.Etfs.Add(new Etf
        //            {
        //                StockSymbol = symbol,
        //                ExDividendDate = exDividendDate,
        //                RecordDate = recordDate,
        //                PaymentDay = paymentDay,
        //                Amount = amount,
        //                Year = year
        //            });
        //        }

        //        try
        //        {

        //            dbContext.SaveChanges();
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }
        //    }
        //}

        //[Test]
        //public void CCC()
        //{
        //    var dbContext = new NarcissusDbContext();
        //    var etfs = dbContext.Etfs.Include("StockSymbolNavigation").Where(x => x.StockSymbolNavigation.Price.HasValue).Select(x => new { Symbol = x.StockSymbolNavigation.Symbol, Amount = x.Amount, Price = x.StockSymbolNavigation.Price.Value }).ToList();

        //    var result = etfs.Select(x => new { x.Symbol, Result = x.Amount / x.Price }).OrderByDescending(x => x.Result).Take(56);

        //    Console.WriteLine(result.Count());
        //}
    }
}
