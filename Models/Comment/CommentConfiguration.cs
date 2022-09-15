using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShopWebApi.Models
{
    public class CommentConfiguration<TEntity> : IEntityTypeConfiguration<Comment<TEntity>>
        where TEntity : class
    {
        private readonly Expression<Func<User, IEnumerable<Comment<TEntity>>>> getAuthorCommentsField;
        private readonly Expression<Func<TEntity, IEnumerable<Comment<TEntity>>>> getObjectCommentsField;
        public CommentConfiguration(
            Expression<Func<User, IEnumerable<Comment<TEntity>>>> getAuthorCommentsField,
            Expression<Func<TEntity, IEnumerable<Comment<TEntity>>>> getObjectCommentsField
        )
        {
            this.getAuthorCommentsField = getAuthorCommentsField;
            this.getObjectCommentsField = getObjectCommentsField;
        }
        public void Configure(EntityTypeBuilder<Comment<TEntity>> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Text).IsRequired();
            builder.Property(c => c.CreationDate).IsRequired();
            builder.HasOne(c => c.Author).WithMany(getAuthorCommentsField).IsRequired();
            builder.HasOne(c => c.Object).WithMany(getObjectCommentsField).IsRequired();
            builder.Property(c => c.UpdateDate).IsRequired(false);
        }
    }
}
