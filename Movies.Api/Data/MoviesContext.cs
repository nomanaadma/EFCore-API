using Microsoft.EntityFrameworkCore;
using Movies.Api.Data.EntityMapping;
using Movies.Api.Models;

namespace Movies.Api.Data;

public class MoviesContext : DbContext
{
	public DbSet<Movie> Movies => Set<Movie>();
	public DbSet<Genre> Genres => Set<Genre>();

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

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration( new GenreMapping() );
		modelBuilder.ApplyConfiguration( new MovieMapping() );

		// modelBuilder.Entity<Movie>()
		// 	.ToTable("Pictures")
		// 	.HasKey(movie => movie.Identifier);
		//
		// modelBuilder.Entity<Movie>().Property(movie => movie.Title)
		// 	.HasColumnType("varchar")
		// 	.HasMaxLength(128)
		// 	.IsRequired();
		//
		// modelBuilder.Entity<Movie>().Property(movie => movie.ReleaseDate)
		// 	.HasColumnType("date");
		//
		// modelBuilder.Entity<Movie>().Property(movie => movie.Synopsis)
		// 	.HasColumnType("varchar")
		// 	.HasColumnName("Plot");

	}
}


