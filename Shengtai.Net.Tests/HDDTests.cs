using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
                weight *= this.Read * 1.0 / 500;
                weight *= this.Write * 1.0 / 500;
                //weight *= this.Year * 1.0 / 3;

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
                new Sdd { Name = "十銓", Money = 2390, Size = 500, Read = 560, Write = 510, Year = 3 },
                new Sdd { Name = "十銓", Money = 4190, Size = 1000, Read = 560, Write = 510, Year = 3 },
                new Sdd { Name = "十銓", Money = 1890, Size = 250, Read = 560, Write = 500, Year = 3 },
                new Sdd { Name = "十銓", Money = 2690, Size = 500, Read = 560, Write = 510, Year = 3 },
                new Sdd { Name = "十銓", Money = 4590, Size = 1000, Read = 560, Write = 510, Year = 3 },

                new Sdd { Name = "UMAX", Money = 749, Size = 240, Read = 520, Write = 450, Year = 3 },
                new Sdd { Name = "UMAX", Money = 1349, Size = 480, Read = 560, Write = 450, Year = 3 },
                new Sdd { Name = "UMAX", Money = 2399, Size = 960, Read = 560, Write = 450, Year = 3 },

                new Sdd { Name = "KLEVV", Money = 550, Size = 120, Read = 500, Write = 380, Year = 3 },
                new Sdd { Name = "KLEVV", Money = 790, Size = 240, Read = 500, Write = 420, Year = 3 },
                new Sdd { Name = "KLEVV", Money = 1390, Size = 480, Read = 540, Write = 490, Year = 3 },
                new Sdd { Name = "KLEVV", Money = 699, Size = 240, Read = 520, Write = 500, Year = 3 },
                new Sdd { Name = "KLEVV", Money = 1278, Size = 480, Read = 520, Write = 500, Year = 3 },    //

                new Sdd { Name = "威剛", Money = 619, Size = 120, Read = 520, Write = 320, Year = 3 },
                new Sdd { Name = "威剛", Money = 849, Size = 240, Read = 520, Write = 450, Year = 3 },
                new Sdd { Name = "威剛", Money = 1549, Size = 480, Read = 520, Write = 450, Year = 3 },
                new Sdd { Name = "威剛", Money = 1090, Size = 256, Read = 560, Write = 520, Year = 3 },
                new Sdd { Name = "威剛", Money = 1790, Size = 512, Read = 560, Write = 520, Year = 3 },
                new Sdd { Name = "威剛", Money = 3190, Size = 1000, Read = 560, Write = 520, Year = 3 },
                new Sdd { Name = "威剛", Money = 6650, Size = 2000, Read = 560, Write = 520, Year = 3 },

                new Sdd { Name = "技嘉", Money = 950, Size = 240, Read = 350, Write = 280, Year = 3 },
                new Sdd { Name = "技嘉", Money = 1350, Size = 256, Read = 530, Write = 500, Year = 5 },
                new Sdd { Name = "技嘉", Money = 1999, Size = 512, Read = 530, Write = 500, Year = 5 },

                new Sdd { Name = "Liteon", Money = 599, Size = 120, Read = 560, Write = 460, Year = 3 },
                new Sdd { Name = "Liteon", Money = 950, Size = 240, Read = 560, Write = 500, Year = 3 },
                new Sdd { Name = "Liteon", Money = 1499, Size = 480, Read = 560, Write = 500, Year = 3 },

                new Sdd { Name = "Intel", Money = 2180, Size = 256, Read = 550, Write = 500, Year = 5 },

                new Sdd { Name = "Pioneer", Money = 619, Size = 120, Read = 520, Write = 400, Year = 3 },
                new Sdd { Name = "Pioneer", Money = 820, Size = 240, Read = 520, Write = 450, Year = 3 },
                new Sdd { Name = "Pioneer", Money = 979, Size = 256, Read = 550, Write = 490, Year = 3 },
                new Sdd { Name = "Pioneer", Money = 1560, Size = 480, Read = 520, Write = 450, Year = 3 },
                new Sdd { Name = "Pioneer", Money = 1660, Size = 512, Read = 550, Write = 490, Year = 3 },
                new Sdd { Name = "Pioneer", Money = 2890, Size = 1000, Read = 550, Write = 500, Year = 3 },

                new Sdd { Name = "WD", Money = 760, Size = 120, Read = 540, Write = 430, Year = 3 },
                new Sdd { Name = "WD", Money = 890, Size = 240, Read = 540, Write = 430, Year = 3 },
                new Sdd { Name = "WD", Money = 1550, Size = 480, Read = 540, Write = 430, Year = 3 },
                new Sdd { Name = "WD", Money = 2880, Size = 1000, Read = 540, Write = 430, Year = 3 },
                new Sdd { Name = "WD", Money = 5650, Size = 2000, Read = 540, Write = 430, Year = 3 },
                new Sdd { Name = "WD", Money = 1190, Size = 250, Read = 545, Write = 525, Year = 5 },
                new Sdd { Name = "WD", Money = 1790, Size = 500, Read = 545, Write = 525, Year = 5 },
                new Sdd { Name = "WD", Money = 3100, Size = 1000, Read = 560, Write = 530, Year = 5 },
                new Sdd { Name = "WD", Money = 7150, Size = 2000, Read = 560, Write = 530, Year = 5 },
                new Sdd { Name = "WD", Money = 14700, Size = 4000, Read = 560, Write = 530, Year = 5 },

                new Sdd { Name = "金士頓", Money = 678, Size = 120, Read = 500, Write = 350, Year = 3 },
                new Sdd { Name = "金士頓", Money = 899, Size = 240, Read = 500, Write = 350, Year = 3 },
                new Sdd { Name = "金士頓", Money = 1599, Size = 480, Read = 500, Write = 450, Year = 3 },
                new Sdd { Name = "金士頓", Money = 2499, Size = 960, Read = 500, Write = 450, Year = 3 },
                new Sdd { Name = "金士頓", Money = 1250, Size = 256, Read = 550, Write = 500, Year = 5 },
                new Sdd { Name = "金士頓", Money = 1588, Size = 512, Read = 550, Write = 520, Year = 5 },
                new Sdd { Name = "金士頓", Money = 3150, Size = 1024, Read = 550, Write = 520, Year = 5 },
                new Sdd { Name = "金士頓", Money = 1239, Size = 240, Read = 550, Write = 480, Year = 3 },
                new Sdd { Name = "金士頓", Money = 1949, Size = 480, Read = 550, Write = 480, Year = 3 },

                new Sdd { Name = "美光", Money = 650, Size = 120, Read = 540, Write = 500, Year = 3 },
                new Sdd { Name = "美光", Money = 850, Size = 240, Read = 540, Write = 500, Year = 3 },
                new Sdd { Name = "美光", Money = 1669, Size = 480, Read = 540, Write = 500, Year = 3 },
                new Sdd { Name = "美光", Money = 2799, Size = 1000, Read = 540, Write = 500, Year = 3 },
                new Sdd { Name = "美光", Money = 5699, Size = 2000, Read = 540, Write = 500, Year = 3 },
                new Sdd { Name = "美光", Money = 1399, Size = 250, Read = 560, Write = 510, Year = 5 },
                new Sdd { Name = "美光", Money = 1888, Size = 500, Read = 560, Write = 510, Year = 5 },
                new Sdd { Name = "美光", Money = 3199, Size = 1000, Read = 560, Write = 510, Year = 5 },
                new Sdd { Name = "美光", Money = 7499, Size = 2000, Read = 560, Write = 510, Year = 5 },

                new Sdd { Name = "Seagate", Money = 1290, Size = 250, Read = 560, Write = 540, Year = 5 },
                new Sdd { Name = "Seagate", Money = 1890, Size = 500, Read = 560, Write = 540, Year = 5 },
                new Sdd { Name = "Seagate", Money = 3390, Size = 1000, Read = 560, Write = 540, Year = 5 },
                new Sdd { Name = "Seagate", Money = 7390, Size = 2000, Read = 560, Write = 540, Year = 5 },

                new Sdd { Name = "三星", Money = 1488, Size = 250, Read = 550, Write = 520, Year = 5 },
                new Sdd { Name = "三星", Money = 1888, Size = 500, Read = 550, Write = 520, Year = 5 },
                new Sdd { Name = "三星", Money = 4750, Size = 1000, Read = 550, Write = 520, Year = 5 },
                new Sdd { Name = "三星", Money = 2788, Size = 256, Read = 560, Write = 530, Year = 5 },
                new Sdd { Name = "三星", Money = 4588, Size = 512, Read = 560, Write = 530, Year = 5 },
                new Sdd { Name = "三星", Money = 9490, Size = 1000, Read = 560, Write = 530, Year = 5 },
                new Sdd { Name = "三星", Money = 15900, Size = 2000, Read = 560, Write = 530, Year = 5 }
            };

            var sdds1 = sdds.OrderBy(s => s.GetWeight());
            foreach(var sdd in sdds1)
                Console.WriteLine($"{sdd.Name}, {sdd.Money}");
        }
    }
}
