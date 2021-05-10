using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Data
{
    public partial class NavDbContext
    {
        private readonly string _connectionString;

        public NavDbContext(string connectionString) : base()
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (string.IsNullOrEmpty(_connectionString))
                    optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=Land;Username=postgres");
                else
                    optionsBuilder.UseNpgsql(_connectionString);
            }
        }
    }
}
