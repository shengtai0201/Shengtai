using System;
using System.Collections.Generic;

#nullable disable

namespace Shengtai.IdentityServer.Data
{
    public partial class MenuRole
    {
        public int MenuId { get; set; }
        public string RoleId { get; set; }

        public virtual Menu Menu { get; set; }
    }
}
