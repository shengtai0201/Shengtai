using System.Threading.Tasks;

namespace Shengtai.Web
{
    //public interface IAccountService<TUser> where TUser : IdentityUser
    public interface IAccountService
    {
        Task<string> FindIdByAccountAsync(string account);
    }
}