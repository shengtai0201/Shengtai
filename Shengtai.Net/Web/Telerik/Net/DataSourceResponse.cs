using System.Web.Mvc;

namespace Shengtai.Web.Telerik.Net
{
    public class DataSourceResponse<TModel> : Shengtai.Web.Telerik.DataSourceResponse<TModel> where TModel : class
    {
        public void SetModelError(ModelStateDictionary modelState)
        {
            foreach (var state in modelState.Values)
                foreach (var error in state.Errors)
                    this.builder.AppendLine(error.ErrorMessage);
        }
    }
}