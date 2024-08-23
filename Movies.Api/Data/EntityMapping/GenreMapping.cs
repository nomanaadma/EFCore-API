using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Api.Data.ValueGenerators;
using Movies.Api.Models;

namespace Movies.Api.Data.EntityMapping;

public class GenreMapping : IEntityTypeConfiguration<Genre>
{
	public void Configure(EntityTypeBuilder<Genre> builder)
	{
		// builder.Property(genre => genre.CreatedDate)
		// 	.HasValueGenerator<CreatedDateGenerator>();	
		
		builder.Property<DateTime>("CreatedDate")
			.HasColumnName("CreatedAt")
			.HasValueGenerator<CreatedDateGenerator>();
		
		//.HasDefaultValue("getdate()");

		builder.Property(g => g.Name)
			.HasMaxLength(256)
			.HasColumnType("varchar");

		builder.Property(g => g.ConcurrencyToken)
			.IsRowVersion();

		builder
			.Property<bool>("Deleted")
			.HasDefaultValue(false);
			
		builder
			.HasQueryFilter(g => EF.Property<bool>(g, "Deleted") == false)
			.HasAlternateKey(g => g.Name);

		// builder.HasData(new Genre { Id = 1, Name = "Drama" });
	}
}