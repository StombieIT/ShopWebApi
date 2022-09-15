using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Models
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasOne(u => u.Role).WithMany(r => r.Users);
            builder.Property(u => u.Login).UseCollation("SQL_Latin1_General_CP1_CS_AS");
            builder.Property(u => u.Password).UseCollation("SQL_Latin1_General_CP1_CS_AS");
        }
    }
}
