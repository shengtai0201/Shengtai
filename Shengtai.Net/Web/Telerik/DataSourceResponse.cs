using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Shengtai.Web.Telerik
{
    public class DataSourceResponse : IErrorDataSource
    {
        protected readonly StringBuilder builder;

        public DataSourceResponse()
        {
            this.builder = new StringBuilder();
        }

        public override string ToString()
        {
            return this.builder.ToString();
        }

        public string ErrorMessage
        {
            get
            {
                return this.ToString();
            }
        }

        public StringBuilder AppendLine(string value)
        {
            return this.builder.AppendLine(value);
        }
    }

    public class DataSourceResponse<TModel> : DataSourceResponse, IDataSourceResponse<TModel> where TModel : class
    {
        //protected readonly StringBuilder builder;

        public DataSourceResponse()
        {
            //this.builder = new StringBuilder();
            this.DataCollection = new List<TModel>();
        }

        public ICollection<TModel> DataCollection { get; set; }

        //public string ErrorMessage
        //{
        //    get
        //    {
        //        return this.ToString();
        //    }
        //}

        public int TotalRowCount { get; set; }

        //public override string ToString()
        //{
        //    return this.builder.ToString();
        //}

        //public StringBuilder AppendLine(string value)
        //{
        //    return this.builder.AppendLine(value);
        //}

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public static DataSourceResponse<TModel> Deserialize(string value)
        {
            DataSourceResponse<TModel> result = null;

            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    result = JsonConvert.DeserializeObject<DataSourceResponse<TModel>>(value);
                }
                catch { }
            }

            return result;
        }
    }
}