using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ShopWebApi.Models
{
    public class ImageConfiguration<TEntity>
        : IEntityTypeConfiguration<Image<TEntity>>
        where TEntity : class
    {
        private readonly Expression<Func<TEntity, IEnumerable<Image<TEntity>>>> getObjectImagesField;
        public ImageConfiguration(Expression<Func<TEntity, IEnumerable<Image<TEntity>>>> getObjectImagesField)
        {
            this.getObjectImagesField = getObjectImagesField;
        }

        public void Configure(EntityTypeBuilder<Image<TEntity>> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.FileName).IsRequired();
            builder
                .HasOne(i => i.Object)
                .WithMany(getObjectImagesField)
                .IsRequired();
        }
    }
}
