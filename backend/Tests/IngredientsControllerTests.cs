using Food_Creator;
using Food_Creator.controller;
using Food_Creator.Model;
using Food_Creator.Model.dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class IngredientsControllerTests
{
    private ApplicationDbContext GetDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
        var context = new ApplicationDbContext(options);
        context.Database.EnsureCreated();
        return context;
    }

    [Fact]
    public async Task GetIngredients_ReturnsAllIngredients()
    {
        // Arrange
        var context = GetDbContext(nameof(GetIngredients_ReturnsAllIngredients));
        context.Ingredients.Add(new Ingredient { IngredientId = 1, Name = "Sugar", Price = 1.1f });
        context.Ingredients.Add(new Ingredient { IngredientId = 2, Name = "Salt", Price = 0.3f });
        await context.SaveChangesAsync();
        var controller = new IngredientsController(context);

        // Act
        var result = await controller.GetIngredients();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var ingredients = Assert.IsAssignableFrom<IEnumerable<IngredientDto>>(okResult.Value);
        Assert.Equal(2, ingredients.Count());
    }

    [Fact]
    public async Task DeleteIngredient_NotExisting_ReturnsNotFound()
    {
        var context = GetDbContext(nameof(DeleteIngredient_NotExisting_ReturnsNotFound));
        var controller = new IngredientsController(context);

        var result = await controller.DeleteIngredient("Unknown");

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateIngredient_ValidData_AddsIngredient()
    {
        var context = GetDbContext(nameof(CreateIngredient_ValidData_AddsIngredient));
        var controller = new IngredientsController(context);
        var dto = new IngredientDto { Name = "Flour", Price = 2.5f };

        var result = await controller.CreateIngredient(dto);

        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        var ingredient = Assert.IsType<Ingredient>(created.Value);
        Assert.Equal("Flour", ingredient.Name);
        Assert.Single(context.Ingredients);
    }
}
