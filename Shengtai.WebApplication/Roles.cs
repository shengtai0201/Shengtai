using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shengtai.WebApplication
{
    [Flags]
    public enum Roles
    {
        Anonymous = 1,

        // 系統營運者
        System = 2,

        // 企業端使用者
        Enterprise = 4,

        // 企業端之最高權限
        Administrator = 8
    }
}
