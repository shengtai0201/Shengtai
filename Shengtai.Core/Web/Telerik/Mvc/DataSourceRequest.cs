using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Shengtai.Web.Telerik.Mvc
{
    [ModelBinder(BinderType = typeof(DataSourceRequestModelBinder))]
    public class DataSourceRequest : IDataSourceRequest
    {
        public IFilterInfoCollection ServerFiltering { get; set; }

        public ServerPageInfo ServerPaging { get; set; }

        public ICollection<ServerSortInfo> ServerSorting { get; set; }
    }
}