using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoppingApp.Infrastructure.Concrete.Ef.Config;

internal class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(s => s.Name).IsRequired().HasMaxLength(128);
        builder.HasOne(s => s.Category).WithMany(s => s.Products).HasForeignKey(s => s.CategoryId);
    }
}
