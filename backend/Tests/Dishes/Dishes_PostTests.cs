using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food_Creator.controller;
using Food_Creator.Model;
using Food_Creator.Model.dto;
using FoodCreator.Tests.Shared;
using Microsoft.AspNetCore.Mvc;
using Xunit;

/// <summary>
/// Poprawne utworzenie dania z istniejącymi składnikami.
/// </summary>
public class Dishes_PostTests
{
    [Fact]
    public async Task CreateDish_WithExistingIngredients_Succeeds()
    {
        var ctx = TestDbContextFactory.Create();
        var cheese = new Ingredient { Name = "Cheese", Price = 1.5f };
        ctx.Ingredients.Add(cheese);
        await ctx.SaveChangesAsync();

        var dto = new DishDto
        {
            Name = "Cheese Sandwich",
            Price = 4,
            DishIngredients = new List<DishIngredientDto>
            {
                new DishIngredientDto { IngredientId = cheese.IngredientId, Quantity = 1 }
            }
        };

        var ctrl = new DishesController(ctx);
        var res  = await ctrl.CreateDish(dto);

        var created = Assert.IsType<CreatedAtActionResult>(res.Result);
        var dish    = Assert.IsType<Dish>(created.Value);
        Assert.Equal("Cheese Sandwich", dish.Name);
        Assert.Single(ctx.DishIngredients);
    }

    /// <summary>
    /// Gdy przekażesz ID składnika, który nie istnieje, metoda powinna zwrócić 500
    /// (tak jest zaimplementowane w kontrolerze).
    /// </summary>
    [Fact]
    public async Task CreateDish_WithInvalidIngredient_Returns500()
    {
        var ctx  = TestDbContextFactory.Create();
        var ctrl = new DishesController(ctx);

        var dto = new DishDto
        {
            Name = "Burger",
            Price = 5,
            DishIngredients = new List<DishIngredientDto>
            {
                new DishIngredientDto { IngredientId = 999, Quantity = 1 }
            }
        };

        var res    = await ctrl.CreateDish(dto);
        var status = Assert.IsType<ObjectResult>(res.Result);
        Assert.Equal(500, status.StatusCode);
    }

    /// <summary>
    /// Pusta nazwa dania → 400 BadRequest.
    /// </summary>
    [Fact]
    public async Task CreateDish_EmptyName_ReturnsBadRequest()
    {
        var ctx  = TestDbContextFactory.Create();
        var ctrl = new DishesController(ctx);

        var dto = new DishDto { Name = "", Price = 1 };
        var res = await ctrl.CreateDish(dto);

        Assert.IsType<BadRequestObjectResult>(res.Result);
    }
}
