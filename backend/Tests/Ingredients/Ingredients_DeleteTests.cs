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
    [Theory(DisplayName = "Usunięcie istniejącego składnika zwraca 200 OK i usuwa go z bazy danych")]
    [InlineData("Salt", 0.1f)]
    [InlineData("Sugar", 0.3f)]
    [InlineData("Pepper", 0.45f)]
    public async Task DeleteIngredient_VariousExistingIngredients_ReturnsOkAndRemoves(string ingredientName, float price)
    {
        var ctx = TestDbContextFactory.Create();
        ctx.Ingredients.Add(new Ingredient { Name = ingredientName, Price = price });
        await ctx.SaveChangesAsync();

        var ctrl = new IngredientsController(ctx);
        var res  = await ctrl.DeleteIngredient(ingredientName);

        Assert.IsType<OkObjectResult>(res.Result);
        Assert.Empty(ctx.Ingredients);
    }

    /// <summary>
    /// Gdy składnik nie istnieje → 404.
    /// </summary>
    [Fact(DisplayName = "Usunięcie nieistniejącego składnika zwraca 404 NotFound")]
    public async Task DeleteIngredient_NotExisting_ReturnsNotFound()
    {
        var ctx  = TestDbContextFactory.Create();
        var ctrl = new IngredientsController(ctx);

        var res = await ctrl.DeleteIngredient("Unknown");

        Assert.IsType<NotFoundObjectResult>(res.Result);
    }
}
