using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Service
{
    public class UserSqlService : IUserSqlService
    {
        private readonly Options.IConnectionStrings _connectionStrings;

        public UserSqlService(Options.IConnectionStrings connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }

        public async Task<string> GetUserIdAsync(string account)
        {
            var connection = new Npgsql.NpgsqlConnection(_connectionStrings.DefaultConnection);
            await connection.OpenAsync();

            var id = await connection.QuerySingleAsync<string>("SELECT \"Id\" FROM \"AspNetUsers\" WHERE \"Account\" = @Account;", new { Account = account });

            await connection.CloseAsync();
            await connection.DisposeAsync();
            return id;
        }
    }
}
