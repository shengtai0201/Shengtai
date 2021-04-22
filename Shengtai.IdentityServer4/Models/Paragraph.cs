using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer4.Models
{
    public class Paragraph
    {
        // 不可為空
        public string Text { get; set; }

        // 可為空
        public string Small { get; set; }

        // 可為空
        public Badge Badge { get; set; }
    }
}
