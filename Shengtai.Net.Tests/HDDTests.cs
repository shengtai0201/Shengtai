using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Tests
{
    [TestFixture]
    class HDDTests
    {
        public HDDTests()
        {

        }

        private class Hdd
        {
            public string Name { get; set; }
            public int Money { get; set; }
            public int Size { get; set; }
            public int Rpm { get; set; }
            public int Year { get; set; }
            public int Cache { get; set; }

            public double GetWeight()
            {
                var weight = this.Size * 1.0 / this.Money;
                //weight *= (this.Rpm * 1.0 / 7200);
                //weight *= (this.Year * 1.0 / 5);
                //weight *= (this.Cache * 1.0 / 512);

                return weight;
            }
        }

        private class Sdd
        {
            public string Name { get; set; }
            public int Money { get; set; }
            public int Size { get; set; }
            public int Read { get; set; }
            public int Write { get; set; }
            public int Year { get; set; }
            //public int Cache { get; set; }

            public double GetWeight()
            {
                var weight = this.Size * 1.0 / this.Money;
                //weight *= (this.Rpm * 1.0 / 7200);
                //weight *= (this.Year * 1.0 / 5);
                //weight *= (this.Cache * 1.0 / 512);

                return weight;
            }
        }

        [Test]
        public void AAA()
        {
            var hdds = new List<Hdd> {
                new Hdd{ Name ="Toshiba", Money=3150, Size=4, Rpm=7200, Year=3, Cache=128 },
                new Hdd{ Name ="Toshiba", Money=2750, Size=4, Rpm=5400, Year=3, Cache=128 },
                new Hdd{ Name ="Toshiba", Money=7350, Size=8, Rpm=7200, Year=3, Cache=256 },
                new Hdd{ Name ="Seagate", Money=2990, Size=4, Rpm=5400, Year=3, Cache=256 },
                new Hdd{ Name ="Seagate", Money=4490, Size=6, Rpm=5400, Year=3, Cache=256 },
                new Hdd{ Name ="Seagate", Money=5690, Size=8, Rpm=5400, Year=3, Cache=256 },
                new Hdd{ Name ="Seagate", Money=6900, Size=8, Rpm=5900, Year=3, Cache=128 },
                new Hdd{ Name ="WD", Money=2950, Size=4, Rpm=5400, Year=3, Cache=64 },

                new Hdd{ Name ="WD", Money=3850, Size=6, Rpm=5400, Year=3, Cache=256 },

                new Hdd{ Name ="WD", Money=5888, Size=4, Rpm=7200, Year=5, Cache=256 },
                new Hdd{ Name ="WD", Money=7750, Size=6, Rpm=7200, Year=5, Cache=256 },
                new Hdd{ Name ="Seagate", Money=3090, Size=4, Rpm=7200, Year=3, Cache=64 },
                new Hdd{ Name ="Seagate", Money=7150, Size=8, Rpm=7200, Year=3, Cache=256 },
                new Hdd{ Name ="Seagate", Money=8890, Size=10, Rpm=7200, Year=3, Cache=256 },
                new Hdd{ Name ="WD", Money=3050, Size=4, Rpm=5400, Year=3, Cache=64 },
                new Hdd{ Name ="WD", Money=4990, Size=6, Rpm=5400, Year=3, Cache=64 },
                new Hdd{ Name ="WD", Money=8350, Size=10, Rpm=7200, Year=3, Cache=256 },
                new Hdd{ Name ="WD", Money=8400, Size=10, Rpm=7200, Year=3, Cache=256 },
                new Hdd{ Name ="Toshiba", Money=3150, Size=4, Rpm=5400, Year=3, Cache=128 },
                new Hdd{ Name ="Toshiba", Money=2850, Size=4, Rpm=5400, Year=3, Cache=128 },
                new Hdd{ Name ="Toshiba", Money=5400, Size=6, Rpm=7200, Year=3, Cache=256 },
                new Hdd{ Name ="Toshiba", Money=7380, Size=8, Rpm=7200, Year=3, Cache=256 },
                new Hdd{ Name ="Toshiba", Money=8500, Size=10, Rpm=7200, Year=3, Cache=256 },

                new Hdd{ Name ="Toshiba", Money=3488, Size=4, Rpm=7200, Year=3, Cache=128 },
                new Hdd{ Name ="Toshiba", Money=5700, Size=6, Rpm=7200, Year=3, Cache=128 },
                new Hdd{ Name ="Toshiba", Money=7350, Size=8, Rpm=7200, Year=3, Cache=128 },
                new Hdd{ Name ="Toshiba", Money=10800, Size=12, Rpm=7200, Year=3, Cache=256 },
                new Hdd{ Name ="Seagate", Money=3488, Size=4, Rpm=5900, Year=3, Cache=64 },
                new Hdd{ Name ="Seagate", Money=5590, Size=6, Rpm=5900, Year=3, Cache=64 },
                new Hdd{ Name ="Seagate", Money=7290, Size=8, Rpm=7200, Year=3, Cache=256 },
                new Hdd{ Name ="Seagate", Money=8990, Size=10, Rpm=7200, Year=3, Cache=256 },
                new Hdd{ Name ="Seagate", Money=10990, Size=12, Rpm=7200, Year=3, Cache=256 },
                new Hdd{ Name ="Seagate", Money=16500, Size=16, Rpm=7200, Year=3, Cache=256 },

                new Hdd{ Name ="Seagate", Money=4990, Size=4, Rpm=7200, Year=5, Cache=128 },
                new Hdd{ Name ="Seagate", Money=6790, Size=6, Rpm=7200, Year=5, Cache=256 },
                new Hdd{ Name ="Seagate", Money=8490, Size=8, Rpm=7200, Year=5, Cache=256 },
                new Hdd{ Name ="Seagate", Money=10490, Size=10, Rpm=7200, Year=5, Cache=256 },
                new Hdd{ Name ="Seagate", Money=12290, Size=12, Rpm=7200, Year=5, Cache=256 },
                new Hdd{ Name ="Seagate", Money=14290, Size=14, Rpm=7200, Year=5, Cache=256 },

                new Hdd{ Name ="WD", Money=3290, Size=4, Rpm=5400, Year=3, Cache=64 },
                new Hdd{ Name ="WD", Money=3450, Size=4, Rpm=5400, Year=3, Cache=256 },
                new Hdd{ Name ="WD", Money=5550, Size=6, Rpm=5400, Year=3, Cache=256 },
                new Hdd{ Name ="WD", Money=7150, Size=8, Rpm=5400, Year=3, Cache=256 },
                new Hdd{ Name ="WD", Money=8990, Size=10, Rpm=5400, Year=3, Cache=256 },
                new Hdd{ Name ="WD", Money=10500, Size=12, Rpm=5400, Year=3, Cache=256 },
                new Hdd{ Name ="WD", Money=12800, Size=14, Rpm=5400, Year=3, Cache=256 },

                new Hdd{ Name ="Seagate", Money=7800, Size=6, Rpm=7200, Year=5, Cache=256 },
                new Hdd{ Name ="Seagate", Money=9790, Size=8, Rpm=7200, Year=5, Cache=256 },
                new Hdd{ Name ="Seagate", Money=12390, Size=10, Rpm=7200, Year=5, Cache=256 },

                new Hdd{ Name ="WD", Money=5400, Size=4, Rpm=7200, Year=5, Cache=256 },
                new Hdd{ Name ="WD", Money=7400, Size=6, Rpm=7200, Year=5, Cache=256 },
                new Hdd{ Name ="WD", Money=9200, Size=8, Rpm=7200, Year=5, Cache=256 },
                new Hdd{ Name ="WD", Money=12990, Size=12, Rpm=7200, Year=5, Cache=256 },
                new Hdd{ Name ="WD", Money=15800, Size=14, Rpm=7200, Year=5, Cache=512 },

                new Hdd{ Name ="WD", Money=5400, Size=4, Rpm=7200, Year=5, Cache=256 },
                new Hdd{ Name ="WD", Money=7200, Size=6, Rpm=7200, Year=5, Cache=256 },
                new Hdd{ Name ="WD", Money=9100, Size=8, Rpm=7200, Year=5, Cache=256 },
                new Hdd{ Name ="WD", Money=10800, Size=10, Rpm=7200, Year=5, Cache=256 },
                new Hdd{ Name ="WD", Money=12900, Size=12, Rpm=7200, Year=5, Cache=256 },
                new Hdd{ Name ="WD", Money=15600, Size=14, Rpm=7200, Year=5, Cache=512 },

                new Hdd{ Name ="Toshiba", Money=4650, Size=4, Rpm=7200, Year=5, Cache=128 },
                new Hdd{ Name ="Toshiba", Money=5888, Size=6, Rpm=7200, Year=5, Cache=128 },
                new Hdd{ Name ="Toshiba", Money=5500, Size=6, Rpm=7200, Year=5, Cache=128 },
                new Hdd{ Name ="Toshiba", Money=6888, Size=8, Rpm=7200, Year=5, Cache=128 },
                new Hdd{ Name ="Toshiba", Money=8888, Size=8, Rpm=7200, Year=5, Cache=256 },
                new Hdd{ Name ="Toshiba", Money=10500, Size=10, Rpm=7200, Year=5, Cache=256 },
                new Hdd{ Name ="Toshiba", Money=12800, Size=12, Rpm=7200, Year=5, Cache=256 },
                new Hdd{ Name ="Toshiba", Money=14700, Size=14, Rpm=7200, Year=5, Cache=256 }
            };

            Hdd h = hdds.First();
            foreach (var hdd in hdds)
            {
                if (hdd.GetWeight() > h.GetWeight())
                    h = hdd;
            }

            string test = string.Empty;
        }

        [Test]
        public void BBB()
        {
            var sdds = new List<Sdd> {
                new Sdd { Name = "十銓", Money = 1850, Size = 500, Read = 560, Write = 510, Year = 3 },
                new Sdd { Name = "十銓", Money = 3390, Size = 1000, Read = 560, Write = 510, Year = 3 },
                new Sdd { Name = "十銓", Money = 1590, Size = 250, Read = 560, Write = 500, Year = 3 },
                new Sdd { Name = "十銓", Money = 4190, Size = 1000, Read = 560, Write = 510, Year = 3 },
                new Sdd { Name = "十銓", Money = 1890, Size = 250, Read = 560, Write = 500, Year = 3 },
            };

            Sdd s = sdds.First();
            foreach (var sdd in sdds)
            {
                if (sdd.GetWeight() > s.GetWeight())
                    s = sdd;
            }

            string test = string.Empty;
        }
    }
}
