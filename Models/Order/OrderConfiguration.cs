using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;

namespace ShopWebApi.Models
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        private readonly int regionMaxLength;
        private readonly int cityMaxLength;
        private readonly int wishesMaxLength;

        public OrderConfiguration() : this(256, 128, 1024)
        {}
        public OrderConfiguration(int regionMaxLength, int cityMaxLength, int wishesMaxLength)
        {
            this.regionMaxLength = regionMaxLength;
            this.cityMaxLength = cityMaxLength;
            this.wishesMaxLength = wishesMaxLength;
        }
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Region).IsRequired().HasMaxLength(regionMaxLength);
            builder.Property(o => o.Locality).IsRequired().HasMaxLength(cityMaxLength);
            builder.Property(o => o.Wishes).HasMaxLength(wishesMaxLength);
            builder.Property(o => o.CreationDate).IsRequired();
            builder
                .HasOne(o => o.ShoppingCart)
                .WithOne(sc => sc.Order)
                .HasForeignKey<Order>(o => o.ShoppingCartId);
        }
    }
}
