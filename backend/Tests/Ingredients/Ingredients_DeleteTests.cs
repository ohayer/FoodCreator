using System.Threading.Tasks;
using Food_Creator.controller;
using Food_Creator.Model;
using FoodCreator.Tests.Shared;
using Microsoft.AspNetCore.Mvc;
using Xunit;

/// <summary>
/// Usuwanie istniejącego składnika powinno zwrócić 200 OK i zmniejszyć liczbę rekordów.
/// </summary>
public class Ingredients_DeleteTests
{
    [Fact]
    public async Task DeleteIngredient_Existing_ReturnsOkAndRemoves()
    {
        var ctx = TestDbContextFactory.Create();
        ctx.Ingredients.Add(new Ingredient { Name = "Pepper", Price = 0.2f });
        await ctx.SaveChangesAsync();

        var ctrl = new IngredientsController(ctx);
        var res  = await ctrl.DeleteIngredient("Pepper");

        Assert.IsType<OkObjectResult>(res.Result);
        Assert.Empty(ctx.Ingredients);
    }

    /// <summary>
    /// Gdy składnik nie istnieje → 404.
    /// </summary>
    [Fact]
    public async Task DeleteIngredient_NotExisting_ReturnsNotFound()
    {
        var ctx  = TestDbContextFactory.Create();
        var ctrl = new IngredientsController(ctx);

        var res = await ctrl.DeleteIngredient("Unknown");

        Assert.IsType<NotFoundObjectResult>(res.Result);
    }
}