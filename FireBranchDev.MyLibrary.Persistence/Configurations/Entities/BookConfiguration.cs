using FireBranchDev.MyLibrary.Domain.Entities;
using FireBranchDev.MyLibrary.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FireBranchDev.MyLibrary.Persistence.Configurations.Entities;

public class BookConfiguration : BaseConfiguration<Book>
{
    public override void Configure(EntityTypeBuilder<Book> builder)
    {
        base.Configure(builder);

        builder.Property(b => b.Isbn)
            .IsRequired()
            .HasMaxLength(13);

        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode();

        builder.Property(b => b.Blurb)
            .IsRequired()
            .HasMaxLength(250)
            .IsUnicode();

        builder.Property(b => b.AuthorFirstName)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode();

        builder.Property(b => b.AuthorLastName)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode();

        builder.Property(b => b.Genre)
            .IsRequired()
            .HasMaxLength(28)
            .IsUnicode();
    }
}
