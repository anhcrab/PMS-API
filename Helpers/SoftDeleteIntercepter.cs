using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using api.Interfaces.Core;

namespace api.Helpers
{
  public class SoftDeleteIntercepter : ISaveChangesInterceptor
  {
    public InterceptionResult<int> SaveChanges(
      DbContextErrorEventData eventData,
      InterceptionResult<int> result
    )
    {
      if (eventData.Context is null) return result;

      foreach (var entry in eventData.Context.ChangeTracker.Entries())
      {
        if (entry is not { State: EntityState.Deleted, Entity: ISoftDelete delete }) continue;
        entry.State = EntityState.Modified;
        delete.IsDeleted = true;
      }
      return result;
    }
  }
}