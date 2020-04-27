using CommonServiceLocator;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Shengtai.Options;
using System.Security.Principal;
using System.Web;

namespace Shengtai.Data.Net
{
    public abstract class Repository<TDbContext, TAppSettings, TDefaultConnection, TUserManager, TUser> :
        Repository<TDbContext, TAppSettings, TDefaultConnection, IPrincipal>, ISecurity<TUserManager, TUser>
        where TDbContext : class
        where TAppSettings : AppSettings<TDefaultConnection>, new()
        where TDefaultConnection : IDefaultConnection
        where TUserManager : UserManager<TUser>
        where TUser : IdentityUser
    {
        public TUserManager UserManager
        {
            get
            {
                TUserManager userManager = null;
                try
                {
                    userManager = ServiceLocator.Current.GetInstance<TUserManager>();
                }
                catch { }

                if (userManager == null)
                    userManager = HttpContext.Current.GetOwinContext().GetUserManager<TUserManager>();

                return userManager;
            }
        }

        private IAuthenticationManager authenticationManager = null;

        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                if (this.authenticationManager == null)
                    this.authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

                return this.authenticationManager;
            }
            set
            {
                this.authenticationManager = value;
            }
        }

        protected Repository() : base()
        {
        }
    }
}