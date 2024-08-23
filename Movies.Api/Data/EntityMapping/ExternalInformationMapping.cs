using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Api.Models;

namespace Movies.Api.Data.EntityMapping;

public class ExternalInformationMapping : IEntityTypeConfiguration<ExternalInformation>
{
    public void Configure(EntityTypeBuilder<ExternalInformation> builder)
    {

	    // builder.HasKey(info => info.MovieId);

	    builder
		    .HasOne(info => info.Movie)
		    .WithOne(movie => movie.ExternalInformation);

    }
}