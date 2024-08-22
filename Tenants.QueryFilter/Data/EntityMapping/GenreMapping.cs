using Tenants.QueryFilter.Data.ValueGenerators;
using Tenants.QueryFilter.Models;
using Tenants.QueryFilter.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tenants.QueryFilter.Data.EntityMapping;

public class GenreMapping : TenantAwareMapping<Genre>
{
    public GenreMapping(MoviesContext context) : base(context)
    { }

    public override void ConfigureEntity(EntityTypeBuilder<Genre> builder)
    {
        builder.Property<DateTime>("CreatedDate")
            .HasColumnName("CreatedAt")
            .HasValueGenerator<CreatedDateGenerator>();
    }
}