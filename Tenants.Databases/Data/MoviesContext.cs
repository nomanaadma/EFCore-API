using Tenants.Databases.Data.EntityMapping;
using Tenants.Databases.Models;
using Tenants.Databases.Tenants;
using Microsoft.EntityFrameworkCore;

namespace Tenants.Databases.Data;

public class MoviesContext : DbContext
{
    public MoviesContext(DbContextOptions<MoviesContext> options)
        :base(options)
    { }
    
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GenreMapping());
    }
}