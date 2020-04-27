using System.Collections;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Shengtai.Net
{
    public static partial class Extensions
    {
        public static void Refresh(this DbContext dbContext, params IEnumerable[] queryables)
        {
            //var entities = queryables.Where(x => x != null).Distinct().ToList();
            ObjectContext context = (dbContext as IObjectContextAdapter).ObjectContext;

            //var collection = (from e in context.ObjectStateManager.GetObjectStateEntries(EntityState.Deleted | EntityState.Modified | EntityState.Unchanged)
            //                  where e.EntityKey != null && entities.Contains(e.Entity)
            //                  select e.Entity).ToList();

            foreach (var collection in queryables)
                context.Refresh(RefreshMode.StoreWins, collection);
        }
    }
}