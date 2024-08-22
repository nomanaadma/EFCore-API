using Tenants.Databases.Data.ValueGenerators;
using Tenants.Databases.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tenants.Databases.Data.EntityMapping;

public class GenreMapping : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property<DateTime>("CreatedDate")
            .HasColumnName("CreatedAt")
            .HasValueGenerator<CreatedDateGenerator>();
    }
}