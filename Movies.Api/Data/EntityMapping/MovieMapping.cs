using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Movies.Api.Data.ValueConverters;
using Movies.Api.Models;

namespace Movies.Api.Data.EntityMapping;

public class MovieMapping : IEntityTypeConfiguration<Movie>
{
	public void Configure(EntityTypeBuilder<Movie> builder)
	{
		builder
			.ToTable("Pictures")
			// .UseTphMappingStrategy()
			.HasQueryFilter(movie => movie.ReleaseDate >= new DateTime(2000, 1, 1))
			.HasKey(movie => movie.Identifier);
	
		builder.Property(movie => movie.Title)
			.HasColumnType("varchar")
			.HasMaxLength(128)
			.IsRequired();

		builder.Property(movie => movie.ReleaseDate)
			.HasColumnType("char(8)")
			.HasConversion(new DateTimeToChar8Converter());
		
		builder.Property(movie => movie.AgeRating)
			.IsRequired();
		
		// builder.ComplexProperty(movie => movie.Director);
		
		// builder.OwnsOne(movie => movie.Director)
		// 	.ToTable("Movie_Directors");
		//
		// builder.OwnsMany(movie => movie.Actors)
		// 	.ToTable("Movie_Actors");
	
		builder.Property(movie => movie.Synopsis)
			.HasColumnType("varchar(max)")
			.HasColumnName("Plot");

		builder.Property(movie => movie.MainGenreName)
			.HasMaxLength(256)
			.HasColumnType("varchar");
		
		builder
			.HasOne(movie => movie.Genre)
			.WithMany(genre => genre.Movies)
			.HasPrincipalKey(genre => genre.Name)
			.HasForeignKey(movie => movie.MainGenreName);
		
		// Seed
		// builder.HasData(new Movie
		// {
		// 	Identifier = 1,
		// 	Title = "Fight Club",
		// 	ReleaseDate = new DateTime(1999, 9, 10),
		// 	Synopsis = "Ed Norton and Brad Pitt have a couple of fist fights with each other.",
		// 	MainGenreId = 1,
		// 	AgeRating = AgeRating.Adolescent
		// });
		//
		// builder.OwnsOne(movie => movie.Director)
		// 	.HasData(new { MovieIdentifier = 1, FirstName = "David", LastName = "Fincher" });
		//
		//
		// builder.OwnsMany(movie => movie.Actors)
		// 	.HasData(
		// 		new { MovieIdentifier = 1, Id = 1, FirstName = "Adward", LastName = "Norton" },
		// 		new { MovieIdentifier = 1, Id = 2, FirstName = "brad", LastName = "Pit" }
		// 	);
		
	}
}

public class CinemaMovieMapping : IEntityTypeConfiguration<CinemaMovie>
{
	public void Configure(EntityTypeBuilder<CinemaMovie> builder)
	{
		// builder.ToTable("CinemaMovie");
	}
}

public class TelevisionMovieMapping : IEntityTypeConfiguration<TelevisionMovie>
{
	public void Configure(EntityTypeBuilder<TelevisionMovie> builder)
	{
		// builder.ToTable("TelevisionMovie");
	}
}