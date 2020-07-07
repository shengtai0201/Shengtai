using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Tests.Exchange
{
    public class Rate
    {
        public DateTime Date { get; set; }

        // 高
        public double Buy { get; set; }

        // 低
        public double Sell { get; set; }

        public static Rate NewInstance(NPOI.SS.UserModel.IRow row)
        {
            var rate = new Rate
            {
                Date = row.GetCell(0).DateCellValue,
                Buy = row.GetCell(1).NumericCellValue,
                Sell = row.GetCell(2).NumericCellValue
            };

            return rate;
        }
    }
}
