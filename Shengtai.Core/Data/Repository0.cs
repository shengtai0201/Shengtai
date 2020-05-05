using System;
using System.Data.Common;

namespace Shengtai.Data
{
    public abstract class Repository : IClient
    {
        private readonly IClient client;

        protected Repository()
        {
            if (CommonServiceLocator.ServiceLocator.IsLocationProviderSet)
                this.client = CommonServiceLocator.ServiceLocator.Current.GetInstance<IClient>();
        }

        public DateTime? DbToDateTime(object value)
        {
            return this.client.DbToDateTime(value);
        }

        public int? DbToInt(object value)
        {
            return this.client.DbToInt(value);
        }

        public string DbToString(object value)
        {
            return this.client.DbToString(value);
        }

        public TParameter GetParameter<TParameter>(string parameterName, string value) where TParameter : DbParameter, new()
        {
            return this.client.GetParameter<TParameter>(parameterName, value);
        }
    }
}