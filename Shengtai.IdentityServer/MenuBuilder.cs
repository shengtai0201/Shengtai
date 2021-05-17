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
        public static IDictionary<Paragraph, int> Menus { get; set; }

        public const int HOME_PAGE_KEY = -1;
        private readonly INavTreeView _homePage;
        public INavTreeView HomePage { get => _homePage; }

        private readonly Data.IDataStrategy _dataStrategy;
        private readonly Task<IList<Menu>> _headers;

        protected MenuBuilder(IAppSettings appSettings, Data.IDataStrategy dataStrategy)
        {
            _homePage = new Menu
            {
                Key = HOME_PAGE_KEY,
                Type = Data.MenuTypes.Item,
                Paragraph = new Paragraph { Text = appSettings.IdentityServer.Text },
                Url = "~/",
                Icon = appSettings.IdentityServer.Icon
            };
            _homePage.ShowEvent += ShowStrategy;

            _dataStrategy = dataStrategy;
            _headers = _dataStrategy.ReadAllAsync(ShowStrategy);
        }

        public void Initialization()
        {
            if (Menus == null)
            {
                Menus = _dataStrategy.Read();
                Menus.Add(_homePage.Paragraph, _homePage.Key);
            }
        }

        public Task<IList<Menu>> GetHeadersAsync()
        {
            return _headers;
        }

        public abstract bool ShowStrategy(Menu sender, IList<string> roles);

        public IList<IMenu> ReadBreadcrumbs(int key)
        {
            IList<IMenu> menus = new List<IMenu> { this.HomePage };

            if (key == HOME_PAGE_KEY)
                return menus;

            _dataStrategy.ReadBreadcrumbs(menus, key);
            return menus;
        }

        public bool IsActived(int activeKey, int targetKey)
        {
            if (activeKey == targetKey)
                return true;

            var breadcrumbs = this.ReadBreadcrumbs(activeKey);
            foreach (var breadcrumb in breadcrumbs)
                if (breadcrumb.Key == targetKey)
                    return true;

            return false;
        }
    }
}
