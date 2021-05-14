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
            this.HomePage.Roles = new[] { "Anonymous" };
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
            Roles menuRoles;
            if (sender.Type == Shengtai.IdentityServer.Data.MenuTypes.Item)
                menuRoles = this.ConvertToRole(sender.Roles);
            else
                menuRoles = this.GetHierarchicalRole(sender.Menus);

            if (roles == null || roles.Count == 0)
                return (menuRoles & Roles.Anonymous) != 0;
            else
            {
                var userRoles = Roles.Anonymous;
                foreach (var value in roles)
                {
                    if (Enum.TryParse(value, out Roles result))
                        userRoles |= result;
                }

                return (menuRoles & userRoles) != 0;
            }
        }
    }
}
