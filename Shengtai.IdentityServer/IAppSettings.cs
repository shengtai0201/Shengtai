using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer
{
    public interface IAppSettings
    {
        public class _IdentityServer
        {
            public string ApplicationName { get; set; } = "AdminLTE";
            public string BrandLogo { get; set; } = "~/_content/Shengtai.IdentityServer/lib/admin-lte/img/AdminLTELogo.png";

            public string Text { get; set; }
            public string Icon { get; set; }

            public ICollection<string> Roles { get; set; }

            public class _Email
            {
                public string Host { get; set; }
                public int Port { get; set; }
                public bool EnableSsl { get; set; }
                public string UserName { get; set; }
                public string Password { get; set; }
                public string From { get; set; }
            }
            public _Email Email { get; set; }

            public class _Configuration
            {
                public string ApiScopeName { get; set; }
                public string ApiScopeDisplayName { get; set; }

                public string ClientId { get; set; }
                public string ClientIdClaimType { get; set; }
                public string ClientSecret { get; set; }
                public string ClientSecretClaimType { get; set; }

                public string Uri { get; set; }

                public string RemoteUri { get; set; }
            }
            public _Configuration Configuration { get; set; }

            public class _Account
            {
                public bool AllowLocalLogin { get; set; } = true;
                public bool AllowRememberLogin { get; set; } = true;
                public TimeSpan RememberMeLoginDuration { get; set; } = TimeSpan.FromDays(30);

                public bool ShowLogoutPrompt { get; set; } = true;
                public bool AutomaticRedirectAfterSignOut { get; set; } = false;

                public string InvalidCredentialsErrorMessage { get; set; } = "Invalid username or password";
            }
            public _Account Account { get; set; }

            public class _Consent
            {
                public bool EnableOfflineAccess { get; set; } = true;
                public string OfflineAccessDisplayName { get; set; } = "Offline Access";
                public string OfflineAccessDescription { get; set; } = "Access to your applications and resources, even when you are offline";

                public string MustChooseOneErrorMessage { get; set; } = "You must pick at least one permission";
                public string InvalidSelectionErrorMessage { get; set; } = "Invalid selection";
            }
            public _Consent Consent { get; set; }
        }

        _IdentityServer IdentityServer { get; }
    }
}
