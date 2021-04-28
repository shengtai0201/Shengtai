using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer
{
    public interface IEmailService : IEmailSender
    {
        Task<bool> SendMailAsync(string subject, string body, params string[] toAddress);
    }
}
