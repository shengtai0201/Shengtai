using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Shengtai.Web.Telerik
{
    public class DataSourceRequest : IDataSourceRequest
    {
        public IFilterInfoCollection ServerFiltering { get; set; }

        public ServerPageInfo ServerPaging { get; set; }

        public ICollection<ServerSortInfo> ServerSorting { get; set; }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public static DataSourceRequest Deserialize(string value)
        {
            DataSourceRequest result = null;

            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    var settings = new JsonSerializerSettings();
                    settings.Converters.Add(new FilterConverter());

                    result = JsonConvert.DeserializeObject<DataSourceRequest>(value, settings);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return result;
        }
    }
}