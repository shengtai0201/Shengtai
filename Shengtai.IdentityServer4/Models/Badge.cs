using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer4.Models
{
    public class Badge
    {
        // 不可為空
        public string Text { get; set; }

        // 不可為空
        public string Class { get; set; }
    }
}
