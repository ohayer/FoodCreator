using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food_Creator.controller;
using Food_Creator.Model;
using FoodCreator.Tests.Shared;
using Microsoft.AspNetCore.Mvc;
using Xunit;

/// <summary>
/// Sprawdza, że GET /api/Ingredients zwraca wszystkich istniejących składników.
/// </summary>
public class Ingredients_GetTests
{
    [Fact]
    public async Task GetIngredients_WhenTwoExist_ReturnsTwoDtos()
    {
        // arrange
        var ctx = TestDbContextFactory.Create();
        ctx.Ingredients.AddRange(
            new Ingredient { Name = "Sugar", Price = 1.1f },
            new Ingredient { Name = "Salt",  Price = 0.3f });
        await ctx.SaveChangesAsync();
        var ctrl = new IngredientsController(ctx);

        // act
        var action = await ctrl.GetIngredients();
        var ok     = Assert.IsType<OkObjectResult>(action.Result);
        var dtos   = Assert.IsAssignableFrom<IEnumerable<IngredientDto>>(ok.Value);

        // assert
        Assert.Equal(2, dtos.Count());
        Assert.Contains(dtos, d => d.Name == "Salt");
    }

    /// <summary>
    /// Gdy w bazie brak danych, endpoint powinien zwrócić OK z pustą listą.
    /// </summary>
    [Fact]
    public async Task GetIngredients_WhenEmpty_ReturnsEmptyList()
    {
        var ctx  = TestDbContextFactory.Create();
        var ctrl = new IngredientsController(ctx);

        var action   = await ctrl.GetIngredients();
        var ok       = Assert.IsType<OkObjectResult>(action.Result);
        var dtos     = Assert.IsAssignableFrom<IEnumerable<IngredientDto>>(ok.Value);

        Assert.Empty(dtos);
    }
}