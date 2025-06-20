using System.Threading.Tasks;
using Food_Creator.controller;
using Food_Creator.Model;
using FoodCreator.Tests.Shared;
using Microsoft.AspNetCore.Mvc;
using Xunit;

/// <summary>
/// Aktualizacja poprawnego składnika zmienia nazwę i cenę.
/// </summary>
public class Ingredients_PutTests
{
    [Fact]
    public async Task UpdateIngredient_Existing_UpdatesAndReturnsOk()
    {
        var ctx = TestDbContextFactory.Create();
        ctx.Ingredients.Add(new Ingredient { IngredientId = 1, Name = "Onion", Price = 0.4f });
        await ctx.SaveChangesAsync();

        var ctrl = new IngredientsController(ctx);
        var update = new Ingredient { IngredientId = 1, Name = "Red onion", Price = 0.6f };

        var res = await ctrl.UpdateIngredient(update);

        Assert.IsType<OkObjectResult>(res);
        var ent = await ctx.Ingredients.FindAsync(1);
        Assert.Equal("Red onion", ent!.Name);
        Assert.Equal(0.6f, ent.Price);
    }

    /// <summary>
    /// Próba aktualizacji nieistniejącego składnika → 404.
    /// </summary>
    [Fact]
    public async Task UpdateIngredient_NotExisting_ReturnsNotFound()
    {
        var ctx  = TestDbContextFactory.Create();
        var ctrl = new IngredientsController(ctx);
        var upd  = new Ingredient { IngredientId = 999, Name = "Ghost", Price = 1 };

        var res = await ctrl.UpdateIngredient(upd);

        Assert.IsType<NotFoundObjectResult>(res);
    }
}