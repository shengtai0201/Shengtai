using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Mvc;

namespace Shengtai.Web.Mvc
{
    // http://james.newtonking.com/archive/2008/10/16/asp-net-mvc-and-json-net
    public class JsonNetResult : JsonResult
    {
        private readonly JsonSerializerSettings settings;
        private readonly Formatting formatting;

        public JsonNetResult(object data)
        {
            this.Data = data;
            this.settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            this.formatting = Formatting.Indented;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (Data != null)
            {
                JsonTextWriter writer = new JsonTextWriter(response.Output) { Formatting = this.formatting };

                JsonSerializer serializer = JsonSerializer.Create(this.settings);
                serializer.Serialize(writer, Data);

                writer.Flush();
            }
        }
    }
}
