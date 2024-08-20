using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.ProuductAgg;

namespace Shop.Infrastructure.Persistent.EF.ProductAgg;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products", "product");
        builder.HasIndex(b => b.Slug).IsUnique();
        builder.Property(b => b.Title).HasMaxLength(200).IsRequired();
        builder.Property(b => b.Description).IsRequired();
        builder.Property(b => b.ImageName).HasMaxLength(110).IsRequired();
        builder.Property(b => b.Slug).IsUnicode(false).IsRequired();

        builder.OwnsOne(b => b.SeoData, config =>
        {
            config.Property(b => b.MetaDescription).HasMaxLength(500).HasColumnName("MetaDescription");

            config.Property(b => b.MetaTitle).HasMaxLength(500).HasColumnName("MetaTitle");

            config.Property(b => b.MetaKeyWords).HasMaxLength(500).HasColumnName("MetaKeyWords");

            config.Property(b => b.IndexPage).HasColumnName("IndexPage");

            config.Property(b => b.Canonical).HasMaxLength(500).HasColumnName("Canonical");

            config.Property(b => b.Schema).HasColumnName("Schema");
        });

        builder.OwnsMany(b => b.Images, option =>
        {
            option.ToTable("Images", "product");
            option.Property(b => b.ImageName).HasMaxLength(100).IsRequired();
        });

        builder.OwnsMany(b => b.Specifications, option =>
        {
            option.ToTable("Specifications", "product");
            option.Property(b => b.Key).HasMaxLength(50).IsRequired();
            option.Property(b => b.Value).HasMaxLength(100).IsRequired();
        });
    }
}
