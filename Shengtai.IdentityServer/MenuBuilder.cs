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
        private readonly IList<INavHeader> _headers;

        protected MenuBuilder()
        {
            _headers = new List<INavHeader>();

            this.Initialize();
        }

        public void AddHeader(INavHeader header)
        {
            _headers.Add(header);
        }

        public IList<INavHeader> GetHeaders()
        {
            return _headers;
        }

        public abstract void Initialize();

        public abstract bool ShowStrategy(Menu sender, IList<string> roles);
    }
}
