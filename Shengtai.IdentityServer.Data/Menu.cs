using System;
using System.Collections.Generic;

#nullable disable

namespace Shengtai.IdentityServer.Data
{
    public partial class Menu
    {
        public Menu()
        {
            InverseParent = new HashSet<Menu>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Text { get; set; }
        public string Small { get; set; }
        public string BadgeText { get; set; }
        public string BadgeClass { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }

        public virtual Menu Parent { get; set; }
        public virtual ICollection<Menu> InverseParent { get; set; }
    }
}
