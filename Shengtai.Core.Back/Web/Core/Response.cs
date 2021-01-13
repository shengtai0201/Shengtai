using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Shengtai.Web.Core
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

        public void SetErrors(IEnumerable<IdentityError> errors)
        {
            if (errors != null)
            {
                StringBuilder builder = new StringBuilder();
                foreach (var error in errors)
                    builder.AppendLine($"Code:{error.Code}, Description:{error.Description}");
                this.Message = builder.ToString();
            }
        }

        public override string ToString()
        {
            return this.builder.ToString();
        }
    }
}
