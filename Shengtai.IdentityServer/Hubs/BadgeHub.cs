using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Hubs
{
    public class BadgeHub : Hub
    {
        public async Task SendMessage(string id)
        {
            var now = DateTime.Now;
            var text = string.Format("{0}:{1} {2}", now.Hour, now.Minute, now.Second);

            await Clients.All.SendAsync("ReceiveMessage", id, text, Models.Shared.Badge.Appearance.Info);
        }
    }
}
