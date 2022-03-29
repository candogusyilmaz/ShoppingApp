using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoppingApp.Infrastructure.Concrete.Ef.Config;

internal class ShoppingListConfig : IEntityTypeConfiguration<ShoppingList>
{
    public void Configure(EntityTypeBuilder<ShoppingList> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(64);
        builder.HasOne(s => s.ActiveShoppingUser).WithMany().HasForeignKey(x => x.ActiveShoppingUserId);
    }
}
