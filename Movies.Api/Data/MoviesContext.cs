using Microsoft.EntityFrameworkCore;
using Movies.Api.Data.EntityMapping;
using Movies.Api.Models;

namespace Movies.Api.Data;

public class MoviesContext(DbContextOptions<MoviesContext> options) : DbContext(options)
{
	public DbSet<Movie> Movies => Set<Movie>();
	public DbSet<Genre> Genres => Set<Genre>();
	
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration( new GenreMapping() );
		modelBuilder.ApplyConfiguration( new MovieMapping() );
		modelBuilder.ApplyConfiguration( new CinemaMovieMapping() );
		modelBuilder.ApplyConfiguration( new TelevisionMovieMapping() );
	}
}


