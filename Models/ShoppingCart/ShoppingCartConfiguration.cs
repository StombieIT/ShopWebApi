using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopWebApi.Models
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(sc => sc.Id);
            builder
                .HasOne(sc => sc.User)
                .WithMany(u => u.ShoppingCarts)
                .IsRequired();
            builder.Property(sc => sc.CreationDate).IsRequired();
        }
    }
}
