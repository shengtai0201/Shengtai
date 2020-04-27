using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace Shengtai.Data.Net
{
    public interface ISecurity<out TUserManager, TUser>
        where TUserManager : UserManager<TUser>
        where TUser : IdentityUser
    {
        IAuthenticationManager AuthenticationManager { get; }
        TUserManager UserManager { get; }
    }
}