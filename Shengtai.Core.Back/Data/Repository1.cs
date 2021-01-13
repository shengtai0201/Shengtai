using System;

namespace Shengtai.Data
{
    public abstract class Repository<TDbContext> : Repository
        where TDbContext : class
    {
        protected TDbContext NewDbContext(string nameOrConnectionString = null)
        {
            if (string.IsNullOrEmpty(nameOrConnectionString))
                return Activator.CreateInstance<TDbContext>();
            else
                return Activator.CreateInstance(typeof(TDbContext), nameOrConnectionString) as TDbContext;
        }
    }
}