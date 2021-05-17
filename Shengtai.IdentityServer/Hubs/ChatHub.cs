using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string text, string @class)
        {
            await Clients.All.SendAsync("ReceiveMessage", text, @class);
        }
    }
}
