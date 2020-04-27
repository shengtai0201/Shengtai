using System.Runtime.Serialization;

namespace Shengtai.Web.Telerik
{
    [DataContract]
    public class ServerPageInfo
    {
        /// <summary>
        /// the page of data item to return (1 means the first page)
        /// </summary>
        [DataMember]
        public int Page { get; set; }

        /// <summary>
        /// the number of items to return
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }

        /// <summary>
        /// how many data items to skip
        /// 可以用於 SQL's BETWEEN @Begin AND @End，@Begin = Skip + 1
        /// </summary>
        [DataMember]
        public int Skip { get; set; }

        /// <summary>
        /// the number of data items to return (the same as pageSize)
        /// 可以用於 SQL's BETWEEN @Begin AND @End，@End = Skip + Take
        /// </summary>
        [DataMember]
        public int Take { get; set; }
    }
}