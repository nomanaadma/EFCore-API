using Microsoft.EntityFrameworkCore;
using Movies.Api.Models;

namespace Movies.Api.Data;

public class MoviesContext : DbContext
{
	public DbSet<Movie> Movies => Set<Movie>();

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer("""
		    Data Source=localhost;
		    Initial Catalog=MoviesDB;
		    User Id=sa;
		    Password=Password123;
		    TrustServerCertificate=True;
		""");
		optionsBuilder.LogTo(Console.WriteLine);
		base.OnConfiguring(optionsBuilder);
	}
}


