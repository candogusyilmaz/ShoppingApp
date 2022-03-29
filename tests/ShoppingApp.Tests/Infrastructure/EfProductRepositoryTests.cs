using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using Moq;

using ShoppingApp.Core.Entities;
using ShoppingApp.Infrastructure;
using ShoppingApp.Infrastructure.Concrete.Ef;

using Xunit;

namespace ShoppingApp.Tests.Infrastructure;
public class EfProductRepositoryTests
{
    private readonly AppDbContext context;

    public EfProductRepositoryTests()
    {
        DbContextOptionsBuilder<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>();
        options.UseInMemoryDatabase(Guid.NewGuid().ToString());

        context = new AppDbContext(options.Options);
    }

    [Fact]
    public async Task Add_ShouldAdd_WhenNameIsUnique()
    {
        // Arrange
        EfProductRepository sut = new EfProductRepository(context);
        Seed();

        // Act
        await sut.Add(new Product() { Name = "A Unique Name" });
        int count = context.Products.Count();

        // Assert
        count.Should().Be(5);
    }

    [Fact]
    public async Task Add_ShouldThrow_WhenNameIsNotUnique()
    {
        // Arrange
        EfProductRepository sut = new EfProductRepository(context);
        Seed();

        // Act
        Func<Task> func = async () => await sut.Add(new Product() { Name = "Product 1" });

        // Assert
        await func.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task Update_ShouldNotUpdate_WhenNameIsNotUnique()
    {
        // Arrange
        EfProductRepository sut = new EfProductRepository(context);
        Seed();

        // Act
        Func<Task> func = async () => await sut.Update(new Product() { Id = 1, Name = "Product 4" });

        // Assert
        await func.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task Update_ShouldUpdate_WhenNameIsUnique()
    {
        // Arrange
        EfProductRepository sut = new EfProductRepository(context);
        Seed();

        // Act
        await sut.Update(new Product() { Id = 1, Name = "Unique Name" });
        Product updatedEntity = context.Products.AsNoTracking().First(s => s.Id == 1);

        // Assert
        updatedEntity.Name.Should().Be("Unique Name");
    }

    [Fact]
    public async Task GetAll_ReturnsWithCategory_WhenCalled()
    {
        // Arrange
        EfProductRepository sut = new EfProductRepository(context);
        Seed();

        // Act
        ICollection<Product> entities = await sut.GetAll();

        // Assert
        entities.Should().HaveCount(4);
        entities.Select(s => s.Category.Should().NotBeNull());
    }

    [Fact]

    public async Task GetAll_ReturnsWithCategory_WhenExpressionGiven()
    {
        // Arrange
        EfProductRepository sut = new EfProductRepository(context);
        Seed();

        // Act
        ICollection<Product> entities = await sut.GetAll(s => s.Id <= 3);

        // Assert
        entities.Should().HaveCount(3);
        entities.Select(s => s.Category.Should().NotBeNull());
    }

    private void Seed()
    {
        context.Categories.AddRange(
            new Category() { Name = "Category 1" },
            new Category() { Name = "Category 2" },
            new Category() { Name = "Category 3" },
            new Category() { Name = "Category 4" },
            new Category() { Name = "Category 5" });

        context.Products.AddRange(
            new Product() { Id = 1, Name = "Product 1", CategoryId = 1, },
            new Product() { Id = 2, Name = "Product 2", CategoryId = 1, },
            new Product() { Id = 3, Name = "Product 3", CategoryId = 2, },
            new Product() { Id = 4, Name = "Product 4", CategoryId = 5, });

        context.SaveChanges();
        context.ChangeTracker.Clear();
    }
}
