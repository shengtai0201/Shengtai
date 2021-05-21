using System;
using System.Collections.Generic;

#nullable disable

namespace Shengtai.IdentityServer.Data
{
    public partial class MenuUser
    {
        public int MenuId { get; set; }
        public string UserId { get; set; }
        public int Push { get; set; }

        public virtual Menu Menu { get; set; }
    }
}
