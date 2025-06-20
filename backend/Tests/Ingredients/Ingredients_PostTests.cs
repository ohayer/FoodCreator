using System.IO;
using System.Threading.Tasks;
using Food_Creator.controller;
using Food_Creator.Model;
using Food_Creator.Model.dto;
using FoodCreator.Tests.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

/// <summary>
/// Waliduje poprawne dodanie składnika bez obrazka.
/// </summary>
public class Ingredients_PostTests
{
    [Fact]
    public async Task CreateIngredient_Valid_NoImage_Succeeds()
    {
        var ctx  = TestDbContextFactory.Create();
        var ctrl = new IngredientsController(ctx);
        var dto  = new IngredientDto { Name = "Flour", Price = 2.5f };

        var action   = await ctrl.CreateIngredient(dto);
        var created  = Assert.IsType<CreatedAtActionResult>(action.Result);
        var entity   = Assert.IsType<Ingredient>(created.Value);

        Assert.Equal("Flour", entity.Name);
        Assert.Single(ctx.Ingredients);
    }

    /// <summary>
    /// Próba dodania składnika bez nazwy powinna zwrócić 400.
    /// </summary>
    [Fact]
    public async Task CreateIngredient_MissingName_ReturnsBadRequest()
    {
        var ctx  = TestDbContextFactory.Create();
        var ctrl = new IngredientsController(ctx);
        var dto  = new IngredientDto { Name = "", Price = 1 };

        var action = await ctrl.CreateIngredient(dto);

        Assert.IsType<BadRequestObjectResult>(action.Result);
        Assert.Empty(ctx.Ingredients);
    }

    /// <summary>
    /// Dodanie składnika z obrazkiem (IFormFile) – sprawdza czy bajty się zapisują.
    /// </summary>
    [Fact]
    public async Task CreateIngredient_WithImage_SavesBytes()
    {
        // przygotuj „udawany” plik 10 bajtów
        var file = new FormFile(new MemoryStream(new byte[10]), 0, 10, "img", "image.jpg")
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/jpeg"
        };

        var dto = new IngredientDto { Name = "Tomato", Price = 0.5f, Image = file };

        var ctx  = TestDbContextFactory.Create();
        var ctrl = new IngredientsController(ctx);
        var res  = await ctrl.CreateIngredient(dto);

        var created = Assert.IsType<CreatedAtActionResult>(res.Result);
        var ent     = Assert.IsType<Ingredient>(created.Value);
        Assert.NotNull(ent.Image);
        Assert.Equal(10, ent.Image!.Length);
    }
}
