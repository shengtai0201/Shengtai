using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Tests.Exchange
{
    public class RateCollection : ICollection<Rate>, IExchange<Rate>
    {
        private readonly IList<Rate> rates;
        private readonly DateTime? start;
        private readonly DateTime? end;

        public RateCollection(DateTime? start, DateTime? end = null)
        {
            this.rates = new List<Rate>();
            
            this.start = start;
            this.end = end;
        }

        public RateCollection(int month)
        {
            this.rates = new List<Rate>();

            var now = DateTime.Now;
            this.start = now.AddMonths(0 - month);
        }

        public int Count => this.rates.Count;

        public bool IsReadOnly => this.rates.IsReadOnly;

        public void Add(Rate rate)
        {
            bool check;
            if (this.end.HasValue && rate.Date <= this.end.Value)
                check = true;
            else if (!this.end.HasValue)
                check = true;
            else
                check = false;

            if (check)
            {
                if (this.start.HasValue && rate.Date >= this.start.Value)
                    check = true;
                else if (!this.start.HasValue)
                    check = true;
                else
                    check = false;

                if (check)
                    this.rates.Add(rate);
            }
        }

        public void Clear()
        {
            this.rates.Clear();
        }

        public bool Contains(Rate rate)
        {
            return this.rates.Contains(rate);
        }

        public void CopyTo(Rate[] array, int arrayIndex)
        {
            this.rates.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Rate> GetEnumerator()
        {
            return this.rates.GetEnumerator();
        }

        public (double, double) GetFullLevel()
        {
            var value = Math.Round(this.rates.Count * 0.8, 0);
            int index = Convert.ToInt32(value);

            var sorted = this.rates.OrderBy(x => x.Buy).ToList();
            return (sorted.Last().Buy, sorted[index].Buy);
        }

        public (double, double) GetLowerLevel()
        {
            var value = Math.Round(this.rates.Count * 0.8, 0);
            int index = Convert.ToInt32(value);

            var sorted = this.rates.OrderByDescending(x => x.Sell).ToList();
            return (sorted[index].Sell, sorted.Last().Sell);
        }

        public bool Remove(Rate rate)
        {
            return this.rates.Remove(rate);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.rates.GetEnumerator() as IEnumerator;
        }
    }
}
