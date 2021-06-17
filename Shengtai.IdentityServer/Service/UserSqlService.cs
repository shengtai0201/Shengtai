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
        private readonly IAppSettings _appSettings;
        private readonly Options.IConnectionStrings _connectionStrings;

        public UserSqlService(IAppSettings appSettings, Options.IConnectionStrings connectionStrings)
        {
            _appSettings = appSettings;
            _connectionStrings = connectionStrings;
        }

        public async Task<string> GetUserIdByClientIdAsync(string clientId)
        {
            var connection = new Npgsql.NpgsqlConnection(_connectionStrings.DefaultConnection);
            await connection.OpenAsync();

            var claimValue = Cryptography.AES.Encrypt(clientId, _appSettings.IdentityServer.Configuration.ClientIdClaimType);
            var id = await connection.QuerySingleAsync<string>("SELECT uc.\"UserId\" FROM \"AspNetUserClaims\" uc WHERE uc.\"ClaimType\" = @ClaimType AND uc.\"ClaimValue\" = @ClaimValue;", new { ClaimType = _appSettings.IdentityServer.Configuration.ClientIdClaimType, ClaimValue = claimValue });

            await connection.CloseAsync();
            await connection.DisposeAsync();
            return id;
        }
    }
}
