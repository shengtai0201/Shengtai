using Shengtai.IdentityServer.Models.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer
{
    public abstract class MenuBuilder
    {
        private readonly INavTreeView _homePage;
        public INavTreeView HomePage { get => _homePage; }

        private readonly Data.IDataStrategy _dataStrategy;
        private readonly Task<IList<Menu>> _headers;

        protected MenuBuilder(IAppSettings appSettings, Data.IDataStrategy dataStrategy)
        {
            _homePage = new Menu
            {
                Type = Data.MenuTypes.Item,
                Paragraph = new Paragraph { Text = appSettings.IdentityServer.Text },
                Url = "~/",
                Icon = appSettings.IdentityServer.Icon,
                Active = true
            };
            _homePage.ShowEvent += ShowStrategy;

            _dataStrategy = dataStrategy;
            _headers = _dataStrategy.ReadAllAsync(ShowStrategy);
        }

        public Task<IList<Menu>> GetHeadersAsync()
        {
            return _headers;
        }

        public abstract bool ShowStrategy(Menu sender, IList<string> roles);

        public abstract bool IsHomePage(string key);

        public IList<IMenu> ReadBreadcrumbs(string key)
        {
            IList<IMenu> menus = new List<IMenu> { this.HomePage };

            if (this.IsHomePage(key))
                return menus;

            _dataStrategy.ReadBreadcrumbs(menus, key);
            return menus;
        }
    }
}
