using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Models.Shared
{
    public class Badge
    {
        public static class Appearance
        {
            public static string Primary { get;  } = "badge-primary";
            public static string Secondary { get; } = "badge-secondary";
            public static string Success { get; } = "badge-success";
            public static string Danger { get; } = "badge-danger";
            public static string Warning { get; } = "badge-warning";
            public static string Info { get; } = "badge-info";
            public static string Light { get; } = "badge-light";
            public static string Dark { get; } = "badge-dark";
        }

        // 不可為空
        public string Text { get; set; }

        // 不可為空
        public string Class { get; set; }
    }
}
