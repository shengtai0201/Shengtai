using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace Shengtai.Web.Mvc
{
    public class XmlResult : ActionResult
    {
        private readonly object model;

        public XmlResult(object model)
        {
            this.model = model;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (this.model == null)
                throw new ArgumentNullException();

            context.HttpContext.Response.Clear();
            context.HttpContext.Response.ContentType = "text/xml";

            var value = JsonConvert.SerializeObject(this.model);
            var document = JsonConvert.DeserializeXmlNode(value);
            document.PrependChild(document.CreateXmlDeclaration("1.0", "utf-8", null));

            var s = document.InnerXml;
            context.HttpContext.Response.Write(s);
        }
    }
}