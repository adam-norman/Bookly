using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");
            builder.HasKey(user => user.Id);
            builder.Property(user => user.FirstName)
                .HasMaxLength(50)
                .HasConversion(user => user.Value, value => new FirstName(value));
            builder.Property(user => user.LastName)
                .HasMaxLength(50)
                .HasConversion(user => user.Value, value => new LastName(value));
            builder.Property(user => user.Email)
                .HasMaxLength(100)
                .HasConversion(email => email.Value, value => new Domain.Entities.Users.Email(value));
            builder.HasIndex(user => user.Email).IsUnique();

        }
    }
}
