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
        private readonly INavTreeView _firstPage;
        public INavTreeView FirstPage { get => _firstPage; }

        private readonly Data.IDataStrategy _dataStrategy;
        private readonly Task<IList<Menu>> _headers;

        protected MenuBuilder(IAppSettings appSettings, Data.IDataStrategy dataStrategy)
        {
            _firstPage = new Menu
            {
                Type = Data.MenuTypes.Item,
                Paragraph = new Paragraph { Text = appSettings.IdentityServer.Text },
                Url = "~/",
                Icon = appSettings.IdentityServer.Icon,
                Active = true
            };
            _firstPage.ShowEvent += ShowStrategy;

            _dataStrategy = dataStrategy;
            _headers = _dataStrategy.ReadAllAsync(ShowStrategy);
        }

        public Task<IList<Menu>> GetHeadersAsync()
        {
            return _headers;
        }

        public abstract bool ShowStrategy(Menu sender, IList<string> roles);

        public IList<IMenu> ReadBreadcrumbs(string key)
        {
            IList<IMenu> menus = new List<IMenu> { this.FirstPage };
            _dataStrategy.ReadBreadcrumbs(menus, key);

            return menus;
        }
    }
}
