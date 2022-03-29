using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using Moq;
using ShoppingApp.Infrastructure.Concrete.Ef;

using ShoppingApp.Infrastructure;

using Xunit;
using ShoppingApp.Core.Entities;

namespace ShoppingApp.Tests.Infrastructure;

public class EfShoppingListProductRepositoryTests : EfRepositoryTestsBase
{
    [Fact]
    public async Task GetAll_ReturnsWithProduct_WhenCalled()
    {
        // Arrange
        var sut = new EfShoppingListProductRepository(context);
        Seed();

        // Act
        var items = await sut.GetAll();

        // Assert
        items.Select(s => s.Product.Should().NotBeNull());
        items.Count.Should().BeGreaterThanOrEqualTo(1);
    }

    [Fact]
    public async Task GetAll_ReturnsWithProduct_WhenCalledWithExpression()
    {
        // Arrange
        var sut = new EfShoppingListProductRepository(context);
        Seed();

        // Act
        var items = await sut.GetAll(s => s.Id <= 2);

        // Assert
        items.Select(s => s.Product.Should().NotBeNull());
        items.Count.Should().Be(2);
    }

    [Fact]
    public async Task GetItemsWithProduct_ReturnsWithProduct_WhenGivenExistingShoppingListId()
    {
        // Arrange
        var sut = new EfShoppingListProductRepository(context);
        Seed();

        // Act
        var items = await sut.GetItemsWithProduct(1);

        // Assert
        items.Select(s => s.Product.Should().NotBeNull());
        items.Should().HaveCount(3);
    }

    [Fact]
    public async Task GetItemsWithProduct_ReturnsWithProduct_WhenGivenNonExistingShoppingListId()
    {
        // Arrange
        var sut = new EfShoppingListProductRepository(context);
        Seed();

        // Act
        var items = await sut.GetItemsWithProduct(2);

        // Assert
        items.Should().BeEmpty();
    }

    [Fact]
    public async Task DeleteProductsFromList_ShouldDeletesShoppingListProducts_WhenGivenExistingShoppingListId()
    {
        // Arrange
        var sut = new EfShoppingListProductRepository(context);
        Seed();

        // Act
        await sut.DeleteProductsFromList(1);
        var count = context.ShoppingListProducts.Count(s => s.ShoppingListId == 1);

        // Assert
        count.Should().Be(0);
    }

    [Fact]
    public async Task DeleteProductsFromList_ShouldNotDeletesShoppingListProducts_WhenGivenNonExistingShoppingListId()
    {
        // Arrange
        var sut = new EfShoppingListProductRepository(context);
        Seed();

        // Act
        await sut.DeleteProductsFromList(2);
        var count = context.ShoppingListProducts.Count(s => s.ShoppingListId == 1);

        // Assert
        count.Should().Be(3);
    }
}
