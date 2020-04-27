using System;
using System.Collections.Generic;
using System.Text;

namespace Shengtai.Data
{
    public interface IData
    {
        DataStatus DataStatus { get; set; }
        int? DataErrorCount { get; set; }
    }
}
