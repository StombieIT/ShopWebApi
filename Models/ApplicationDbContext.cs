using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartProduct> ShoppingCartProducts { get; set; }
        public DbSet<Comment<Product>> ProductComments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Image<Product>> ProductImages { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new ShoppingCartConfiguration());
            builder.ApplyConfiguration(new ShoppingCartProductConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration<Product>(u => u.ProductComments, p => p.Comments));
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new ImageConfiguration<Product>(p => p.Images));
            base.OnModelCreating(builder);
        }
    }
}
