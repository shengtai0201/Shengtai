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
                weight *= this.Year * 1.0 / 3;

                return weight;
            }
        }

        [Test]
        public void AAA()
        {
            var hdds = new List<Hdd> {
                new Hdd{ Name ="Toshiba", Money=1230, Size=1, Rpm=7200 },
                new Hdd{ Name ="Toshiba", Money=1790, Size=2, Rpm=7200 },
                new Hdd{ Name ="Toshiba", Money=2450, Size=3, Rpm=7200 },
                new Hdd{ Name ="Toshiba", Money=3120, Size=4, Rpm=7200 },
                new Hdd{ Name ="Toshiba", Money=2750, Size=4, Rpm=5400 },
                new Hdd{ Name ="Toshiba", Money=4950, Size=6, Rpm=7200 },
                new Hdd{ Name ="Toshiba", Money=6490, Size=8, Rpm=7200 },
                new Hdd{ Name ="Toshiba", Money=7790, Size=10, Rpm=7200 },

                new Hdd{ Name ="Seagate", Money=1250, Size=1, Rpm=7200 },
                new Hdd{ Name ="Seagate", Money=1730, Size=2, Rpm=7200 },
                new Hdd{ Name ="Seagate", Money=2490, Size=3, Rpm=5400 },
                new Hdd{ Name ="Seagate", Money=2850, Size=4, Rpm=5400 },
                new Hdd{ Name ="Seagate", Money=4188, Size=6, Rpm=5400 },
                new Hdd{ Name ="Seagate", Money=5300, Size=8, Rpm=5400 },

                new Hdd{ Name ="WD", Money=1250, Size=1, Rpm=7200 },
                new Hdd{ Name ="WD", Money=1650, Size=2, Rpm=5400 },
                new Hdd{ Name ="WD", Money=2590, Size=3, Rpm=5400 },
                new Hdd{ Name ="WD", Money=2950, Size=4, Rpm=5400 },
                new Hdd{ Name ="WD", Money=2750, Size=4, Rpm=5400 },
                new Hdd{ Name ="WD", Money=4450, Size=6, Rpm=5400 },

                new Hdd{ Name ="WD", Money=2300, Size=1, Rpm=7200 },
                new Hdd{ Name ="WD", Money=3399, Size=2, Rpm=7200 },
                new Hdd{ Name ="WD", Money=5290, Size=4, Rpm=7200 },
                new Hdd{ Name ="WD", Money=6780, Size=6, Rpm=7200 },
                new Hdd{ Name ="WD", Money=8350, Size=7, Rpm=7200 },
                new Hdd{ Name ="WD", Money=9390, Size=10, Rpm=7200 },

                new Hdd{ Name ="Seagate", Money=1350, Size=1, Rpm=5900 },
                new Hdd{ Name ="Seagate", Money=1860, Size=2, Rpm=5900 },
                new Hdd{ Name ="Seagate", Money=2490, Size=3, Rpm=5400 },
                new Hdd{ Name ="Seagate", Money=2990, Size=4, Rpm=5900 },
                new Hdd{ Name ="Seagate", Money=6690, Size=8, Rpm=7200 },
                new Hdd{ Name ="Seagate", Money=8590, Size=10, Rpm=7200 },
                new Hdd{ Name ="Seagate", Money=9550, Size=12, Rpm=7200 },

                new Hdd{ Name ="WD", Money=1380, Size=1, Rpm=5400 },
                new Hdd{ Name ="WD", Money=1850, Size=2, Rpm=5400 },
                new Hdd{ Name ="WD", Money=2520, Size=3, Rpm=5400 },
                new Hdd{ Name ="WD", Money=2980, Size=4, Rpm=5400 },
                new Hdd{ Name ="WD", Money=4950, Size=6, Rpm=5400 },
                new Hdd{ Name ="WD", Money=6500, Size=8, Rpm=7200 },
                new Hdd{ Name ="WD", Money=8300, Size=10, Rpm=7200 },

                new Hdd{ Name ="Toshiba", Money=1350, Size=1, Rpm=5700 },
                new Hdd{ Name ="Toshiba", Money=1888, Size=2, Rpm=5700 },
                new Hdd{ Name ="Toshiba", Money=2500, Size=3, Rpm=5940 },
                new Hdd{ Name ="Toshiba", Money=3150, Size=4, Rpm=5400 },
                new Hdd{ Name ="Toshiba", Money=2850, Size=4, Rpm=5400 },
                new Hdd{ Name ="Toshiba", Money=5480, Size=6, Rpm=7200 },
                new Hdd{ Name ="Toshiba", Money=7890, Size=10, Rpm=7200 }
            };


            var hs = hdds.OrderByDescending(x => x.GetWeight()).ToList();

            //Hdd h = hdds.First();
            //foreach (var hdd in hdds)
            //{
            //    if (hdd.GetWeight() > h.GetWeight())
            //        h = hdd;
            //}

            string test = string.Empty;
        }

        [Test]
        public void BBB()
        {
            var sdds = new List<Sdd> {
                new Sdd { Name = "十銓", Money = 1850, Size = 500, Read = 560, Write = 510, Year = 3 },
                new Sdd { Name = "十銓", Money = 3390, Size = 1000, Read = 560, Write = 510, Year = 3 },
                new Sdd { Name = "十銓", Money = 1399, Size = 512, Read = 550, Write = 500, Year = 3 },
                new Sdd { Name = "十銓", Money = 2450, Size = 1000, Read = 550, Write = 500, Year = 3 },
                new Sdd { Name = "十銓", Money = 2390, Size = 500, Read = 560, Write = 510, Year = 3 },
                new Sdd { Name = "十銓", Money = 4190, Size = 1000, Read = 560, Write = 510, Year = 3 },
                new Sdd { Name = "十銓", Money = 2690, Size = 500, Read = 560, Write = 510, Year = 3 },
                new Sdd { Name = "十銓", Money = 4590, Size = 1000, Read = 560, Write = 510, Year = 3 },

                new Sdd { Name = "UMAX", Money = 739, Size = 240, Read = 520, Write = 450, Year = 3 },
                new Sdd { Name = "UMAX", Money = 1350, Size = 480, Read = 560, Write = 450, Year = 3 },
                new Sdd { Name = "UMAX", Money = 2550, Size = 960, Read = 560, Write = 450, Year = 3 },

                new Sdd { Name = "KLEVV", Money = 450, Size = 120, Read = 500, Write = 380, Year = 3 },
                new Sdd { Name = "KLEVV", Money = 750, Size = 240, Read = 500, Write = 420, Year = 3 },
                new Sdd { Name = "KLEVV", Money = 1188, Size = 480, Read = 540, Write = 490, Year = 3 },
                new Sdd { Name = "KLEVV", Money = 850, Size = 256, Read = 560, Write = 520, Year = 3 },
                new Sdd { Name = "KLEVV", Money = 1550, Size = 512, Read = 560, Write = 520, Year = 3 },
                new Sdd { Name = "KLEVV", Money = 2850, Size = 1000, Read = 560, Write = 520, Year = 3 },

                new Sdd { Name = "威剛", Money = 549, Size = 120, Read = 520, Write = 320, Year = 3 },
                new Sdd { Name = "威剛", Money = 790, Size = 240, Read = 520, Write = 450, Year = 3 },
                new Sdd { Name = "威剛", Money = 1399, Size = 480, Read = 520, Write = 450, Year = 3 },
                new Sdd { Name = "威剛", Money = 999, Size = 256, Read = 560, Write = 520, Year = 3 },
                new Sdd { Name = "威剛", Money = 1559, Size = 512, Read = 560, Write = 520, Year = 3 },
                new Sdd { Name = "威剛", Money = 2850, Size = 1000, Read = 560, Write = 520, Year = 3 },
                new Sdd { Name = "威剛", Money = 5888, Size = 2000, Read = 560, Write = 520, Year = 3 },

                new Sdd { Name = "技嘉", Money = 790, Size = 240, Read = 500, Write = 420, Year = 3 },

                new Sdd { Name = "Liteon", Money = 499, Size = 120, Read = 560, Write = 460, Year = 3 },
                new Sdd { Name = "Liteon", Money = 780, Size = 240, Read = 560, Write = 500, Year = 3 },
                new Sdd { Name = "Liteon", Money = 1349, Size = 480, Read = 560, Write = 500, Year = 3 },

                new Sdd { Name = "Intel", Money = 1999, Size = 256, Read = 550, Write = 500, Year = 5 },

                new Sdd { Name = "Pioneer", Money = 549, Size = 120, Read = 520, Write = 400, Year = 3 },
                new Sdd { Name = "Pioneer", Money = 769, Size = 240, Read = 520, Write = 450, Year = 3 },
                new Sdd { Name = "Pioneer", Money = 979, Size = 256, Read = 550, Write = 490, Year = 3 },
                new Sdd { Name = "Pioneer", Money = 1399, Size = 480, Read = 520, Write = 450, Year = 3 },
                new Sdd { Name = "Pioneer", Money = 1555, Size = 512, Read = 550, Write = 490, Year = 3 },
                new Sdd { Name = "Pioneer", Money = 2750, Size = 1000, Read = 550, Write = 500, Year = 3 },

                new Sdd { Name = "WD", Money = 660, Size = 120, Read = 540, Write = 430, Year = 3 },
                new Sdd { Name = "WD", Money = 850, Size = 240, Read = 540, Write = 430, Year = 3 },
                new Sdd { Name = "WD", Money = 1460, Size = 480, Read = 540, Write = 430, Year = 3 },
                new Sdd { Name = "WD", Money = 2690, Size = 1000, Read = 540, Write = 430, Year = 3 },
                new Sdd { Name = "WD", Money = 5490, Size = 2000, Read = 540, Write = 430, Year = 3 },
                new Sdd { Name = "WD", Money = 1090, Size = 250, Read = 545, Write = 525, Year = 5 },
                new Sdd { Name = "WD", Money = 1690, Size = 500, Read = 545, Write = 525, Year = 5 },
                new Sdd { Name = "WD", Money = 2990, Size = 1000, Read = 560, Write = 530, Year = 5 },
                new Sdd { Name = "WD", Money = 6650, Size = 2000, Read = 560, Write = 530, Year = 5 },
                new Sdd { Name = "WD", Money = 14290, Size = 4000, Read = 560, Write = 530, Year = 5 },

                new Sdd { Name = "金士頓", Money = 578, Size = 120, Read = 500, Write = 350, Year = 3 },
                new Sdd { Name = "金士頓", Money = 770, Size = 240, Read = 500, Write = 350, Year = 3 },
                new Sdd { Name = "金士頓", Money = 1330, Size = 480, Read = 500, Write = 450, Year = 3 },
                new Sdd { Name = "金士頓", Money = 2550, Size = 960, Read = 500, Write = 450, Year = 3 },
                new Sdd { Name = "金士頓", Money = 1050, Size = 256, Read = 550, Write = 500, Year = 5 },
                new Sdd { Name = "金士頓", Money = 1680, Size = 512, Read = 550, Write = 520, Year = 5 },
                new Sdd { Name = "金士頓", Money = 3200, Size = 1024, Read = 550, Write = 520, Year = 5 },
                new Sdd { Name = "金士頓", Money = 5688, Size = 2048, Read = 550, Write = 520, Year = 5 },

                new Sdd { Name = "美光", Money = 590, Size = 120, Read = 540, Write = 500, Year = 3 },
                new Sdd { Name = "美光", Money = 800, Size = 240, Read = 540, Write = 500, Year = 3 },
                new Sdd { Name = "美光", Money = 1450, Size = 480, Read = 540, Write = 500, Year = 3 },
                new Sdd { Name = "美光", Money = 2600, Size = 1000, Read = 540, Write = 500, Year = 3 },
                new Sdd { Name = "美光", Money = 5699, Size = 2000, Read = 540, Write = 500, Year = 3 },
                new Sdd { Name = "美光", Money = 1299, Size = 250, Read = 560, Write = 510, Year = 5 },
                new Sdd { Name = "美光", Money = 1550, Size = 500, Read = 560, Write = 510, Year = 5 },
                new Sdd { Name = "美光", Money = 2899, Size = 1000, Read = 560, Write = 510, Year = 5 },
                new Sdd { Name = "美光", Money = 5999, Size = 2000, Read = 560, Write = 510, Year = 5 },

                new Sdd { Name = "Seagate", Money = 1070, Size = 250, Read = 560, Write = 540, Year = 5 },
                new Sdd { Name = "Seagate", Money = 1650, Size = 500, Read = 560, Write = 540, Year = 5 },
                new Sdd { Name = "Seagate", Money = 2990, Size = 1000, Read = 560, Write = 540, Year = 5 },
                new Sdd { Name = "Seagate", Money = 6450, Size = 2000, Read = 560, Write = 540, Year = 5 },

                new Sdd { Name = "三星", Money = 1288, Size = 250, Read = 550, Write = 520, Year = 5 },
                new Sdd { Name = "三星", Money = 1688, Size = 500, Read = 550, Write = 520, Year = 5 },
                new Sdd { Name = "三星", Money = 3288, Size = 1000, Read = 550, Write = 520, Year = 5 },
                new Sdd { Name = "三星", Money = 2599, Size = 1000, Read = 560, Write = 530, Year = 3 },
                new Sdd { Name = "三星", Money = 2288, Size = 256, Read = 560, Write = 530, Year = 5 },
                new Sdd { Name = "三星", Money = 3488, Size = 512, Read = 560, Write = 530, Year = 5 },
                new Sdd { Name = "三星", Money = 7688, Size = 1000, Read = 560, Write = 530, Year = 5 },
                new Sdd { Name = "三星", Money = 13900, Size = 2000, Read = 560, Write = 530, Year = 5 }
            };

            var sdds1 = sdds.OrderByDescending(s => s.GetWeight());
            foreach(var sdd in sdds1)
                Console.WriteLine($"{sdd.Name}, {sdd.Money}");
        }

        private class MicroSD
        {
            public string Name { get; set; }
            public int Money { get; set; }
            public int Size { get; set; }

            public double GetWeight()
            {
                var weight = this.Size * 1.0 / this.Money;

                return weight;
            }
        }

        [Test]
        public void CCC()
        {
            var sdds = new List<MicroSD> {
                new MicroSD { Name = "三星", Money = 199, Size = 64 },    // .3216
                new MicroSD { Name = "三星", Money = 399, Size = 128 },   // .3208
                new MicroSD { Name = "三星", Money = 888, Size = 256 },   // .2882

                new MicroSD { Name = "創見", Money = 112, Size = 16 },    // .1428
                new MicroSD { Name = "創見", Money = 138, Size = 32 },    // .2318
                new MicroSD { Name = "創見", Money = 208, Size = 64 },    // .3076

                new MicroSD { Name = "威剛", Money = 105, Size = 16 },    // .1523
                new MicroSD { Name = "威剛", Money = 108, Size = 32 },    // .2962
                new MicroSD { Name = "威剛", Money = 178, Size = 64 },    // .3595    1
                new MicroSD { Name = "威剛", Money = 360, Size = 128 },   // .3555    3
                new MicroSD { Name = "威剛", Money = 720, Size = 256 }    // .3555    2
            };

            var sdds1 = sdds.OrderByDescending(s => s.GetWeight());
            foreach (var sdd in sdds1)
                Console.WriteLine($"{sdd.Name}, {sdd.Money}");
        }
    }
}
