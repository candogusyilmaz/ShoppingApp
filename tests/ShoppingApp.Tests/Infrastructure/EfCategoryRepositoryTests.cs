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

public class EfCategoryRepositoryTests
{
    private readonly AppDbContext context;

    public EfCategoryRepositoryTests()
    {
        DbContextOptionsBuilder<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>();
        options.UseInMemoryDatabase(Guid.NewGuid().ToString());

        context = new AppDbContext(options.Options);
    }

    [Fact]
    public async Task GetAll_ReturnsAllCategories()
    {
        // Arrange
        EfCategoryRepository sut = new EfCategoryRepository(context);
        Seed();

        // Act
        List<Category> categories = await sut.GetAll();

        // Assert
        categories.Should().HaveCount(5);
    }

    [Fact]
    public async Task GetAll_ReturnsSingleCategory_WhenCorrectIdGiven()
    {
        // Arrange
        EfCategoryRepository sut = new EfCategoryRepository(context);
        Seed();

        // Act
        List<Category> categories = await sut.GetAll(s => s.Id == 1);

        // Assert
        categories.Should().HaveCount(1);
    }

    [Fact]
    public async Task Get_ReturnsCategory_WhenCorrectIdGiven()
    {
        // Arrange
        EfCategoryRepository sut = new EfCategoryRepository(context);
        Seed();

        // Act
        Category category = await sut.Get(s => s.Id == 1);

        // Assert
        category.Should().NotBeNull();
    }

    [Fact]
    public async Task Get_NotReturnsCategory_WhenIncorrectIdGiven()
    {
        // Arrange
        EfCategoryRepository sut = new EfCategoryRepository(context);
        Seed();

        // Act
        Category category = await sut.Get(s => s.Id == 8);

        // Assert
        category.Should().BeNull();
    }

    [Fact]
    public async Task AddCategory_ShouldAddCategory_WhenOnlyNameGiven()
    {
        //Arrange
        EfCategoryRepository sut = new EfCategoryRepository(context);
        Seed();

        //Act
        await sut.Add(new Category() { Name = "A Unique Name" });
        List<Category> users = context.Categories.AsNoTracking().ToList();

        //Assert
        users.Should().HaveCount(6);
    }

    [Fact]
    public async Task UpdateCategory_ShouldUpdateCategoryName()
    {
        //Arrange
        EfCategoryRepository sut = new EfCategoryRepository(context);
        Seed();

        Category category = context.Categories.First(s => s.Id == 2);
        category.Name = "Changed";

        //Act
        await sut.Update(category);
        Category updatedCategory = context.Categories.AsNoTracking().First(s => s.Id == 2);

        //Assert
        updatedCategory.Id.Should().Be(2);
        updatedCategory.Name.Should().Be("Changed");
    }

    [Fact]
    public async Task DeleteCategory_ShouldDeleteCategory_WhenOnlyIdGiven()
    {
        //Arrange
        EfCategoryRepository sut = new EfCategoryRepository(context);
        Seed();

        //Act
        await sut.Delete(new Category() { Id = 1 });
        List<Category> categories = context.Categories.ToList();

        //Assert
        categories.Should().HaveCount(4);
    }


    private void Seed()
    {
        context.Categories.AddRange(
            new Category { Id = 1, Name = "Category 1" },
            new Category { Id = 2, Name = "Category 2" },
            new Category { Id = 3, Name = "Category 3" },
            new Category { Id = 4, Name = "Category 4" },
            new Category { Id = 5, Name = "Category 5" });
        context.SaveChanges();
        context.ChangeTracker.Clear();
    }
}
