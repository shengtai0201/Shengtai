using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Tests.Exchange
{
    public interface IExchange<T>
    {
        void Add(T item);

        (double, double) GetFullLevel();

        (double, double) GetLowerLevel();
    }
}
