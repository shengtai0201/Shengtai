using System.IO;
using System.Web.Mvc;

namespace Shengtai.Net
{
    public static partial class Extensions
    {
        public static string RenderViewToString(this ControllerContext controllerContext, string partialViewName, object model)
        {
            if (string.IsNullOrEmpty(partialViewName))
                partialViewName = controllerContext.RouteData.GetRequiredString("action");

            var tempData = new ViewDataDictionary(model);

            using (var writer = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controllerContext, partialViewName);
                var viewContext = new ViewContext(controllerContext, viewResult.View, tempData, new TempDataDictionary(), writer);
                viewResult.View.Render(viewContext, writer);

                return writer.GetStringBuilder().ToString();
            }
        }

        public static JsonResult JsonNet(this Controller controller, object data)
        {
            return new Web.Mvc.JsonNetResult(data);
        }
    }
}