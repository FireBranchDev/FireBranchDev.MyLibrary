using FireBranchDev.MyLibrary.Domain.Common;
using FireBranchDev.MyLibrary.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FireBranchDev.MyLibrary.Persistence.Configurations.Common;

public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Base
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.UseTpcMappingStrategy();

        builder.Property(b => b.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(b => b.Created)
            .HasColumnName("CreatedUtc")
            .IsRequired()
            .HasConversion<DateTimeUtcValueConverter>();

        builder.Property(b => b.Updated)
            .HasColumnName("UpdatedUtc")
            .IsRequired()
            .HasConversion<DateTimeUtcValueConverter>();
    }
}
