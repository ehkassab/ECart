using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace Ordering.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context!);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context!);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public void UpdateEntities(DbContext context)
        {
            if (context is null) return;

            foreach (var item in context.ChangeTracker.Entries<IEntity>())
            {
                if(item.State == EntityState.Added)
                {
                    item.Entity.CreatedBy = "e";
                    item.Entity.CreatedDate = DateTime.UtcNow;
                }
                else if (item.State == EntityState.Added || item.State == EntityState.Modified || item.HasChangedOwnedEntities())
                {
                    item.Entity.UpdatedBy = "e";
                    item.Entity.UpdatedDate = DateTime.UtcNow;
                }
            }
        }
    }

    public static class Extensions
    {
         public static bool HasChangedOwnedEntities(this EntityEntry entry)
         {
            return entry.References.Any(r=>r.TargetEntry != null &&
                                 r.TargetEntry.Metadata.IsOwned() &&
                                (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
        }
    }
}
