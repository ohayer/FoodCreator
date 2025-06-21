using System.Threading.Tasks;
using Food_Creator.controller;
using Food_Creator.Model;
using FoodCreator.Tests.Shared;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Aktualizacja poprawnego składnika zmienia nazwę i cenę.
/// </summary>
public class Ingredients_PutTests
{
    [Theory]
    [InlineData("UpdatedSalt", 1.5f)]
    [InlineData("UpdatedSugar", 2.0f)]
    [InlineData("UpdatedFlour", 2.5f)]
    public async Task UpdateIngredient_InlineData_UpdatesCorrectly(string newName, float newPrice)
    {
        var ctx = TestDbContextFactory.Create();
        ctx.Ingredients.Add(new Ingredient { IngredientId = 1, Name = "Old", Price = 1.0f });
        await ctx.SaveChangesAsync();

        var ctrl = new IngredientsController(ctx);
        var update = new Ingredient { IngredientId = 1, Name = newName, Price = newPrice };

        var res = await ctrl.UpdateIngredient(update);

        Assert.IsType<OkObjectResult>(res);
        var updated = await ctx.Ingredients.FindAsync(1);
        Assert.Equal(newName, updated!.Name);
        Assert.Equal(newPrice, updated.Price);
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
    /// <summary>
    /// Testowanie z MemberData
    /// </summary>
    public static IEnumerable<object[]> IngredientUpdateMemberData =>
        new List<object[]>
        {
            new object[] { "RiceUpdated", 0.9f },
            new object[] { "TomatoUpdated", 1.2f }
        };

    [Theory]
    [MemberData(nameof(IngredientUpdateMemberData))]
    public async Task UpdateIngredient_MemberData_UpdatesCorrectly(string name, float price)
    {
        var ctx = TestDbContextFactory.Create();
        ctx.Ingredients.Add(new Ingredient { IngredientId = 1, Name = "Initial", Price = 1.0f });
        await ctx.SaveChangesAsync();

        var ctrl = new IngredientsController(ctx);
        var update = new Ingredient { IngredientId = 1, Name = name, Price = price };

        var res = await ctrl.UpdateIngredient(update);

        Assert.IsType<OkObjectResult>(res);
        var updated = await ctx.Ingredients.FindAsync(1);
        Assert.Equal(name, updated!.Name);
        Assert.Equal(price, updated.Price);
    }
    
    /// <summary>
    /// Testowanie z zewnętrznych danych
    /// </summary>
    [Theory]
    [ClassData(typeof(IngredientUpdateJsonData))]
    public async Task UpdateIngredient_JsonData_UpdatesCorrectly(string name, float price)
    {
        var ctx = TestDbContextFactory.Create();
        ctx.Ingredients.Add(new Ingredient { IngredientId = 1, Name = "Base", Price = 1.0f });
        await ctx.SaveChangesAsync();

        var ctrl = new IngredientsController(ctx);
        var update = new Ingredient { IngredientId = 1, Name = name, Price = price };

        var res = await ctrl.UpdateIngredient(update);

        Assert.IsType<OkObjectResult>(res);
        var updated = await ctx.Ingredients.FindAsync(1);
        Assert.Equal(name, updated!.Name);
        Assert.Equal(price, updated.Price);
    }
}
