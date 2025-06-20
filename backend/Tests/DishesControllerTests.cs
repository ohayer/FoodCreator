using Food_Creator;
using Food_Creator.controller;
using Food_Creator.Model;
using Food_Creator.Model.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class DishesControllerTests
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
    public async Task GetDishes_ReturnsAllDishes()
    {
        var context = GetDbContext(nameof(GetDishes_ReturnsAllDishes));
        context.Dishes.Add(new Dish { DishId = 1, Name = "Pizza", Price = 10 });
        await context.SaveChangesAsync();
        var controller = new DishesController(context);

        var actionResult = await controller.GetDishes();

        var ok = Assert.IsType<OkObjectResult>(actionResult);
        var result = Assert.IsAssignableFrom<IEnumerable<object>>(ok.Value);
        Assert.Single(result);
    }

    [Fact]
    public async Task CreateDish_InvalidIngredient_ReturnsError()
    {
        var context = GetDbContext(nameof(CreateDish_InvalidIngredient_ReturnsError));
        var controller = new DishesController(context);
        var dto = new DishDto
        {
            Name = "Burger",
            Price = 5,
            DishIngredients = new List<DishIngredientDto>
            {
                new DishIngredientDto { IngredientId = 999, Quantity = 1 }
            }
        };

        var result = await controller.CreateDish(dto);

        var status = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, status.StatusCode);
    }
}
