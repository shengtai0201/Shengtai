using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Shengtai.Data
{
    // just for ado.net
    public interface IClient
    {
        int? DbToInt(object value);

        string DbToString(object value);

        DateTime? DbToDateTime(object value);

        TParameter GetParameter<TParameter>(string parameterName, string value) where TParameter : DbParameter, new();
    }
}