using Microsoft.AspNet.Identity.EntityFramework;

namespace Shengtai.Web
{
    public interface ICheckPasswordService<TUser> where TUser : IdentityUser
    {
        TUser FindById(string id);
    }
}