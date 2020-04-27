using System.Collections.Generic;

namespace Shengtai.Web.Telerik
{
    public interface IDataSourceRequest
    {
        IFilterInfoCollection ServerFiltering { get; }

        ServerPageInfo ServerPaging { get; }

        ICollection<ServerSortInfo> ServerSorting { get; set; }
    }
}