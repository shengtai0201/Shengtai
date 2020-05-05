using System.Threading.Tasks;

namespace Shengtai.Web
{
    //public interface IAccountService<TUser> where TUser : IdentityUser
    public interface IAccountService
    {
        //void SetConnectionString(string connectionString);
        //string GetConnectionString();

        Task<string> FindIdByAccountAsync(string account);
        //Task<string> FindIdByAccountAsync(string connectionString, string account);

        bool DatabaseInitialized { get; }
        bool ApplicationDbInitializer();
    }
}