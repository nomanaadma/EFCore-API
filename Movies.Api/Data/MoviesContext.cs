using Microsoft.EntityFrameworkCore;
using Movies.Api.Data.EntityMapping;
using Movies.Api.Data.Interceptors;
using Movies.Api.Models;

namespace Movies.Api.Data;

public class MoviesContext(DbContextOptions<MoviesContext> options) : DbContext(options)
{
	public DbSet<Genre> Genres => Set<Genre>();
	public DbSet<Movie> Movies => Set<Movie>();
	public DbSet<ExternalInformation> ExternalInformation => Set<ExternalInformation>();
	public DbSet<Actor> Actors => Set<Actor>();
	
	// public DbSet<GenreName> GenreNames => Set<GenreName>();
	
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration( new GenreMapping() );
		modelBuilder.ApplyConfiguration( new MovieMapping() );
		modelBuilder.ApplyConfiguration( new CinemaMovieMapping() );
		modelBuilder.ApplyConfiguration( new TelevisionMovieMapping() );
		modelBuilder.ApplyConfiguration( new ExternalInformationMapping() );
		modelBuilder.ApplyConfiguration( new ActorMapping() );
	}
	
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.AddInterceptors(new SaveChangesInterceptor());
	}
}


