using Microsoft.EntityFrameworkCore;
using Shengtai.IdentityServer.Models.Shared;
using Shengtai.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Data
{
    public class DatabaseStrategy : IDataStrategy
    {
        private readonly IConnectionStrings _connectionStrings;
        private readonly IRoleService _roleService;

        public DatabaseStrategy(IConnectionStrings connectionStrings, IRoleService roleService)
        {
            _connectionStrings = connectionStrings;
            _roleService = roleService;
        }

        private async Task<IList<Models.Shared.Menu>> ReadAllAsync(IMenu.ShowEventHandler showStrategy, (Menu Entity, Models.Shared.Menu ViewModel) parent, IList<Menu> entities)
        {
            var menus = new List<Models.Shared.Menu>();

            if (parent.Entity == null)
            {
                var headers = entities.Where(m => m.Type == MenuTypes.Header).OrderBy(m => m.Id).ToList();
                foreach (var header in headers)
                {
                    var menu = new Models.Shared.Menu
                    {
                        Key = header.Id,
                        Type = MenuTypes.Header,
                        Text = header.Text
                    };
                    menu.ShowEvent += showStrategy;

                    await this.ReadAllAsync(showStrategy, (header, menu), entities);
                    menus.Add(menu);
                }
            }
            else
            {
                INavHeader headerParent = parent.Entity.Type == MenuTypes.Header ? parent.Entity as INavHeader : null;
                INavTreeView treeViewParent = parent.Entity.Type == MenuTypes.TreeView ? parent.Entity as INavTreeView : null;

                var nextEntities = entities.Where(m => m.ParentId == parent.Entity.Id).ToList();
                foreach (var entity in nextEntities)
                {
                    var menu = new Models.Shared.Menu { Parent = (headerParent, treeViewParent) };

                    if (entity.Type == MenuTypes.TreeView)
                    {
                        menu.Key = entity.Id;
                        menu.Type = MenuTypes.TreeView;
                        menu.Paragraph = new Paragraph { Small = entity.Small, Text = entity.Text };
                        menu.Url = entity.Url;
                        menu.Icon = entity.Icon;
                        menu.ShowEvent += showStrategy;

                        await this.ReadAllAsync(showStrategy, (entity, menu), entities);
                    }
                    else if (entity.Type == MenuTypes.Item)
                    {
                        menu.Key = entity.Id;
                        menu.Type = MenuTypes.Item;
                        menu.Paragraph = new Paragraph { Small = entity.Small, Text = entity.Text };
                        menu.Url = entity.Url;
                        menu.Icon = entity.Icon;
                        menu.ShowEvent += showStrategy;
                        menu.Roles = await this.GetRolesAsync(entity.Id);
                    }

                    parent.ViewModel.Menus.Add(menu);
                }
            }

            return menus;
        }

        public async Task<IList<Models.Shared.Menu>> ReadAllAsync(IMenu.ShowEventHandler showStrategy)
        {
            var dbContext = new NavDbContext(_connectionStrings.DefaultConnection);
            var entities = await dbContext.Menus.OrderBy(m => m.Id).ToListAsync();

            var menus = await this.ReadAllAsync(showStrategy, (null, null), entities);
            return menus;
        }

        private void ReadBreadcrumbs(IList<IMenu> menus, Menu entity)
        {
            if (entity != null)
            {
                if (entity.ParentId.HasValue)
                {
                    var dbContext = new NavDbContext(_connectionStrings.DefaultConnection);
                    var parent = dbContext.Menus.SingleOrDefault(m => m.Id == entity.ParentId.Value);
                    if (parent != null)
                        this.ReadBreadcrumbs(menus, parent);
                }

                var menu = new Models.Shared.Menu();
                switch (entity.Type)
                {
                    case MenuTypes.Header:
                        menu.Key = entity.Id;
                        menu.Text = entity.Text;
                        menu.Type = MenuTypes.Header;
                        break;
                    case MenuTypes.TreeView:
                        menu.Key = entity.Id;
                        menu.Paragraph = new Paragraph { Text = entity.Text };
                        menu.Type = MenuTypes.TreeView;
                        break;
                    case MenuTypes.Item:
                        menu.Key = entity.Id;
                        menu.Url = entity.Url;
                        menu.Paragraph = new Paragraph { Text = entity.Text };
                        menu.Type = MenuTypes.Item;
                        break;
                }
                menus.Add(menu);
            }
        }

        public void ReadBreadcrumbs(IList<IMenu> menus, int key)
        {
            var dbContext = new NavDbContext(_connectionStrings.DefaultConnection);
            var entity = dbContext.Menus.SingleOrDefault(m => m.Id == key);

            this.ReadBreadcrumbs(menus, entity);
        }

        public IDictionary<Paragraph, int> Read()
        {
            IDictionary<Paragraph, int> result = new Dictionary<Paragraph, int>();
            var dbContext = new NavDbContext(_connectionStrings.DefaultConnection);

            var menus = dbContext.Menus.Include("Parent").Where(m => m.Type == MenuTypes.Item).ToList();
            foreach (var menu in menus)
                result.Add(new Paragraph { Text = menu.Text, Small = menu.Small, Parent = new Paragraph { Text = menu.Parent.Text, Small = menu.Parent.Small } }, menu.Id);

            return result;
        }

        public async Task<IList<string>> GetRolesAsync(int menuId)
        {
            IList<string> result = new List<string>();

            var dbContext = new NavDbContext(_connectionStrings.DefaultConnection);
            var roleIds = await dbContext.MenuRoles.Where(x => x.MenuId == menuId).Select(x => x.RoleId).ToListAsync();
            foreach (var roleId in roleIds)
            {
                var role = await _roleService.FindByIdAsync(roleId);
                result.Add(role.Name);
            }

            return result;
        }
    }
}
