using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Models
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        private readonly int titleMaxLength;
        public ProductConfiguration() : this(256)
        {}
        public ProductConfiguration(int titleMaxLength)
        {
            this.titleMaxLength = titleMaxLength;
        }
        public void Configure(EntityTypeBuilder<Product> builder)
        {
  
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title).HasMaxLength(titleMaxLength).IsRequired();
            builder.Property(p => p.CreationDate).IsRequired();
        }
    }
}
