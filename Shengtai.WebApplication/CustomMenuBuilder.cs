using Shengtai.IdentityServer;
using Shengtai.IdentityServer.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shengtai.WebApplication
{
    public class CustomMenuBuilder : MenuBuilder
    {
        public CustomMenuBuilder(IAppSettings appSettings, IdentityServer.Data.IDataStrategy dataStrategy) : base(appSettings, dataStrategy)
        {
            this.FirstPage.Key = 0;
            this.FirstPage.Roles = new[] { "Anonymous" };
        }

        private Roles ConvertToRole(IList<string> roles)
        {
            Roles? r = null;

            foreach(var role in roles)
            {
                if (Enum.TryParse(role, out Roles result))
                {
                    if (r.HasValue)
                        r |= result;
                    else
                        r = result;
                }
            }

            if(!r.HasValue)
                throw new ArgumentException("請檢查選單設定");
            return r.Value;
        }

        private Roles GetHierarchicalRole(ICollection<INavTreeView> menus)
        {
            Roles? r = null;

            foreach (var menu in menus)
            {
                if (menu.Type == IdentityServer.Data.MenuTypes.TreeView)
                {
                    if (r.HasValue)
                        r |= GetHierarchicalRole(menu.Menus);
                    else
                        r = GetHierarchicalRole(menu.Menus);
                }
                else
                {
                    if (r.HasValue)
                        r |= this.ConvertToRole(menu.Roles);
                    else
                        r = this.ConvertToRole(menu.Roles);
                }
            }

            if (!r.HasValue)
                throw new ArgumentException("請檢查選單設定");
            return r.Value;
        }

        public override bool ShowStrategy(Menu sender, IList<string> roles)
        {
            Roles role;
            if (sender.Type == IdentityServer.Data.MenuTypes.Item)
                role = this.ConvertToRole(sender.Roles);
            else
                role = this.GetHierarchicalRole(sender.Menus);

            if (roles == null || roles.Count == 0)
                return (role & Roles.Anonymous) == Roles.Anonymous;
            else
            {
                var r = Roles.Anonymous;
                foreach (var value in roles)
                {
                    if (Enum.TryParse(value, out Roles result))
                        r |= result;
                }

                return (role & r) == r;
            }
        }
    }
}
