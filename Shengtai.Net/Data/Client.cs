using System;
using System.Data.Common;

namespace Shengtai.Data
{
    public class Client : IClient
    {
        public int? DbToInt(object value)
        {
            if (value == DBNull.Value)
                return null;
            else
                return Convert.ToInt32(value);
        }

        public string DbToString(object value)
        {
            if (value == DBNull.Value)
                return null;
            else
                return value.ToString();
        }

        public DateTime? DbToDateTime(object value)
        {
            if (value == DBNull.Value)
                return null;
            else
                return Convert.ToDateTime(value);
        }

        public TParameter GetParameter<TParameter>(string parameterName, string value) where TParameter : DbParameter, new()
        {
            if (string.IsNullOrEmpty(value))
                return Activator.CreateInstance(typeof(TParameter), parameterName, DBNull.Value) as TParameter;
            else
                return Activator.CreateInstance(typeof(TParameter), parameterName, value) as TParameter;
        }
    }
}