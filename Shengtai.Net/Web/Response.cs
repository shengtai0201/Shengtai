using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Shengtai.Web
{
    [DataContract]
    public class Response<T> : IDataSource
    {
        [DataMember]
        public T Result { get; set; }

        [DataMember]
        public string Message
        {
            get
            {
                return this.ToString();
            }
            set
            {
                this.builder = new StringBuilder(value);
            }
        }

        [DataMember]
        public DateTime Time { get; set; }

        private StringBuilder builder;

        public Response()
        {
            this.builder = new StringBuilder();
        }

        public StringBuilder AppendLine(string value)
        {
            return this.builder.AppendLine(value);
        }

        public override string ToString()
        {
            return this.builder.ToString();
        }
    }
}
