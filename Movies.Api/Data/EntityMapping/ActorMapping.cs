﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Api.Models;

namespace Movies.Api.Data.EntityMapping;

public class ActorMapping : IEntityTypeConfiguration<Actor>
{
	public void Configure(EntityTypeBuilder<Actor> builder)
	{
		builder.HasMany(actor => actor.Movies)
			.WithMany(movie => movie.Actors)
			.UsingEntity("Movie_Actor",
				left => left.HasOne(typeof(Movie))
					.WithMany()
					.HasForeignKey("MovieId")
					.HasPrincipalKey(nameof(Movie.Identifier))
					.HasConstraintName("FK_MovieActor_Movie")
					.OnDelete(DeleteBehavior.Cascade),
				right => right.HasOne(typeof(Actor))
					.WithMany()
					.HasForeignKey("ActorId")
					.HasPrincipalKey(nameof(Actor.Id))
					.HasConstraintName("FK_MovieActor_Actor")
					.OnDelete(DeleteBehavior.Cascade),
				linkBuilder => linkBuilder.HasKey("MovieId", "ActorId"));
		
	}
}