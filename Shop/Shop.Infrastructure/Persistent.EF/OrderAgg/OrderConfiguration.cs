using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.OrderAgg;

namespace Shop.Infrastructure.Persistent.EF.OrderAgg;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders", "order");

        builder.OwnsOne(b => b.Discount, option =>
        {
            option.Property(b => b.DiscountTitle).HasMaxLength(50);
        });

        builder.OwnsOne(b => b.ShippingMethod, option =>
        {
            option.Property(b => b.ShippingType).HasMaxLength(50);
        });

        builder.OwnsMany(b => b.Items, option =>
        {
            option.ToTable("Items", "order");
        });

        builder.OwnsOne(b => b.Address, option =>
        {
            option.ToTable("Addresses", "order");
            option.HasKey(b => b.Id);

            option.Property(b=>b.City).HasMaxLength(50).IsRequired();
            option.Property(b=>b.Shire).HasMaxLength(50).IsRequired();
            option.Property(b => b.PhoneNumber).HasMaxLength(11).IsRequired();
            option.Property(b => b.Family).HasMaxLength(100).IsRequired();
            option.Property(b => b.Name).HasMaxLength(100).IsRequired();
            option.Property(b => b.NationalCode).HasMaxLength(10).IsRequired();
            option.Property(b => b.PostalCode).HasMaxLength(40).IsRequired();
            option.Property(b => b.PostalAddress).HasMaxLength(40).IsRequired();
        });
    }
}
