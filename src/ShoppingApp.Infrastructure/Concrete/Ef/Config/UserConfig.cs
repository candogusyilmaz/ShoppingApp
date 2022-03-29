using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoppingApp.Infrastructure.Concrete.Ef.Config;

internal class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(s => s.Email).IsUnique();
        builder.Property(s => s.FirstName).IsRequired().HasMaxLength(64);
        builder.Property(s => s.LastName).IsRequired().HasMaxLength(64);
        builder.Property(s => s.Email).IsRequired().HasMaxLength(255);
        builder.Property(s => s.Password).IsRequired().HasMaxLength(24);
        builder.Property(s => s.Role).IsRequired().HasDefaultValue(Core.Enums.UserRoles.Basic);
    }
}
