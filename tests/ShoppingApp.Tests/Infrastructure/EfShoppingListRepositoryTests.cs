using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using Moq;

using ShoppingApp.Core.Entities;
using ShoppingApp.Infrastructure;

using ShoppingApp.Infrastructure.Concrete.Ef;

using Xunit;

namespace ShoppingApp.Tests.Infrastructure;

public class EfShoppingListRepositoryTests : EfRepositoryTestsBase
{
    [Fact]
    public async Task GetShoppingLists_ReturnsUsersShoppingLists_WhenUserHasShoppingLists()
    {
        // Arrange
        var sut = new EfShoppingListRepository(context);
        Seed();

        // Act
        var result = await sut.GetShoppingLists(1);

        // Assert
        result.Should().HaveCount(_shoppingListUsers.Count(s => s.UserId == 1));
    }

    [Fact]
    public async Task GetShoppingLists_ReturnsNullOrEmpty_WhenUserDoesNotHaveShoppingLists()
    {
        // Arrange
        var sut = new EfShoppingListRepository(context);
        Seed();

        // Act
        var result = await sut.GetShoppingLists(3);

        // Assert
        result.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task ShoppingListWithProducts_ReturnsUsersShoppingListWithShoppingListProducts_WhenUserHasShoppingList()
    {
        // Arrange
        var sut = new EfShoppingListRepository(context);
        Seed();

        // Act
        var result = await sut.ShoppingListWithProducts(1, 1);

        // Assert
        result.ShoppingListProducts.Should().HaveCount(_shoppingListProducts.Count(s => s.ShoppingListId == 1));
        result.ShoppingListProducts.Select(s => s.Product.Should().NotBeNull());
    }

    [Fact]
    public async Task ShoppingListWithProducts_ReturnsNull_WhenUserHasNoShoppingLists()
    {
        // Arrange
        var sut = new EfShoppingListRepository(context);
        Seed();

        // Act
        var result = await sut.ShoppingListWithProducts(3, -1);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task ShoppingListWithProducts_ReturnsEmptyShoppingList_WhenUserHasShoppingListButDoesNotHaveShoppingListProducts()
    {
        // Arrange
        var sut = new EfShoppingListRepository(context);
        Seed();

        // Act
        var result = await sut.ShoppingListWithProducts(1, 2);

        // Assert
        result.Should().NotBeNull();
        result.ShoppingListProducts.Count.Should().Be(0);
    }

    [Fact]
    public async Task DeleteShoppingList_DeletesEverything_WhenGivenExistingShoppingListId()
    {
        // Arrange
        var sut = new EfShoppingListRepository(context);
        Seed();

        // Act
        await sut.DeleteShoppingList(1);

        // Assert
        context.ShoppingLists.AsNoTracking().Any(s => s.Id == 1).Should().BeFalse();
        context.ShoppingListProducts.AsNoTracking().Any(s => s.ShoppingListId == 1).Should().BeFalse();
        context.ShoppingListUsers.AsNoTracking().Any(s => s.ShoppingListId == 1).Should().BeFalse();
    }
}
