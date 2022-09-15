using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopWebApi.Models
{
    public class ShoppingCartProductConfiguration : IEntityTypeConfiguration<ShoppingCartProduct>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartProduct> builder)
        {
            builder.HasKey(scp => scp.Id);
            builder
                .HasOne(scp => scp.ShoppingCart)
                .WithMany(sc => sc.ShoppingCartProducts)
                .IsRequired();
            builder
                .HasOne(scp => scp.Product)
                .WithMany(p => p.ShoppingCartProducts)
                .IsRequired();
            builder.Property(scp => scp.CreationDate).IsRequired();
        }
    }
}
