using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shengtai.Tests
{
    [TestFixture]
    public class MiscellaneousTests
    {
        public struct DateTime
        {
            public int Hour { get; set; }
            public int Minute { get; set; }
            public int Second { get; set; }

            public void Add()
            {
                this.Second++;
                if (this.Second >= 60)
                {
                    this.Second = 0;
                    this.Minute++;
                    if (this.Minute >= 60)
                    {
                        this.Minute = 0;
                        this.Hour++;
                        if (this.Hour >= 24)
                            this.Hour = 0;
                    }
                }
            }

            public void Minus()
            {
                this.Second--;
                if (this.Second < 0)
                {
                    if (this.Minute > 0)
                    {
                        this.Second = 59;
                        this.Minute--;
                        if (this.Minute < 0)
                        {
                            if (this.Hour > 0)
                            {
                                this.Minute = 59;
                                this.Hour--;
                                if (this.Hour < 0)
                                {
                                    this.Hour = 0;
                                }
                            }
                            else
                                this.Minute = 0;
                        }
                    }
                    else
                        this.Second = 0;
                }
            }

            public void WriteLine()
            {
                Console.WriteLine($"Hour: {this.Hour}, Minute: {this.Minute}, Second: {this.Second}");
            }
        }

        [Test]
        public void AAA()
        {
            var dt = new DateTime();
            dt.WriteLine();
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(10);
                dt.Add();
                dt.WriteLine();
            }
            for (int i = 0; i < 120; i++)
            {
                Thread.Sleep(10);
                dt.Minus();
                dt.WriteLine();
            }

            var test = string.Empty;
        }
    }
}
