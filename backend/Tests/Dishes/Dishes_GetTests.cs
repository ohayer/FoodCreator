using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Food_Creator.controller;
using Food_Creator.Model;
using FoodCreator.Tests.Shared;
using Microsoft.AspNetCore.Mvc;
using Xunit;

file record DishView(string DishName, IEnumerable<IngredientView> Ingredients);
file record IngredientView(string IngredientName, int Quantity, float IngredientPrice);
/// <summary>
/// Gdy w bazie nie ma dań, GET /api/Dishes zwraca pustą listę.
/// </summary>
public class Dishes_GetTests
{
    /// <summary>
    /// Sprawdza, że GET /api/Dishes zwraca pustą listę, gdy nie ma żadnych dań.
    /// </summary>
    [Fact]
    public async Task GetDishes_NoData_ReturnsEmpty()
    {
        var ctx  = TestDbContextFactory.Create();
        var ctrl = new DishesController(ctx);

        var res = await ctrl.GetDishes();
        var ok  = Assert.IsType<OkObjectResult>(res);
        var list = Assert.IsAssignableFrom<IEnumerable<object>>(ok.Value);

        Assert.Empty(list);
    }

    /// <summary>
    /// Sprawdzcza, że GET /api/Dishes zwraca dania z ich składnikami.
    /// </summary>
    [Fact]
    public async Task GetDishes_WithData_ReturnsDishAndIngredients()
    {
        // ---------- arrange ----------
        var ctx  = TestDbContextFactory.Create();

        var cheese = new Ingredient { Name = "Cheese",  Price = 1.5f };
        var tomato = new Ingredient { Name = "Tomato",  Price = 0.5f };
        var dish   = new Dish       { Name = "Pizza",   Price = 10   };

        ctx.Ingredients.AddRange(cheese, tomato);
        ctx.Dishes.Add(dish);
        ctx.DishIngredients.AddRange(
            new DishIngredient { Dish = dish, Ingredient = cheese, Quantity = 1 },
            new DishIngredient { Dish = dish, Ingredient = tomato, Quantity = 2 });
        await ctx.SaveChangesAsync();

        var ctrl = new DishesController(ctx);

        // ---------- act ----------
        var actionResult = await ctrl.GetDishes();
        var ok           = Assert.IsType<OkObjectResult>(actionResult);

        // serializacja → deserializacja do silnie typowanego DTO
        var json   = JsonSerializer.Serialize(ok.Value);
        var dishes = JsonSerializer.Deserialize<List<DishView>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

        // ---------- assert ----------
        Assert.Single(dishes);
        var first = dishes[0];
        Assert.Equal("Pizza", first.DishName);
        Assert.Equal(2, first.Ingredients.Count());
    }
}