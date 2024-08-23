using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Movies.Api.Models;

namespace Movies.Api.Data.Interceptors;

public class SaveChangesInterceptor : ISaveChangesInterceptor
{
    public InterceptionResult<int> SavingChanges(
        DbContextEventData eventData, 
        InterceptionResult<int> result)
    {
        if (eventData.Context is not MoviesContext context) return result;

        var tracker = context.ChangeTracker;

        var deleteEntries = tracker.Entries<Genre>()
            .Where(entry => entry.State == EntityState.Deleted);

        foreach (var deleteEntry in deleteEntries)
        {
            deleteEntry.Property<bool>("Deleted").CurrentValue = true;
            deleteEntry.State = EntityState.Modified;
        }

        return result;
    }

    public ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        return ValueTask.FromResult(SavingChanges(eventData, result));
    }
}