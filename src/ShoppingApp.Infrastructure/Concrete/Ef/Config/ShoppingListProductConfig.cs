using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoppingApp.Infrastructure.Concrete.Ef.Config;

internal class ShoppingListProductConfig : IEntityTypeConfiguration<ShoppingListProduct>
{
    public void Configure(EntityTypeBuilder<ShoppingListProduct> builder)
    {
        builder.HasOne(s => s.Product).WithMany()
               .HasForeignKey(s => s.ProductId).IsRequired();
        builder.HasOne(s => s.ShoppingList).WithMany(s => s.ShoppingListProducts)
               .HasForeignKey(s => s.ShoppingListId).IsRequired();

        builder.Property(s => s.Description).HasMaxLength(255);
    }
}
