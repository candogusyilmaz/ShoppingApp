
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoppingApp.Infrastructure.Concrete.Ef.Config;

internal class ShoppingListUserConfig : IEntityTypeConfiguration<ShoppingListUser>
{
    public void Configure(EntityTypeBuilder<ShoppingListUser> builder)
    {
        builder.HasOne(s => s.User).WithMany().HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(s => s.ShoppingList).WithMany(s => s.ShoppingListUsers).HasForeignKey(s => s.ShoppingListId).OnDelete(DeleteBehavior.NoAction);
    }
}
