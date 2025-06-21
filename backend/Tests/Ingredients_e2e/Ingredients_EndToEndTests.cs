using System.Threading.Tasks;
using Food_Creator.Model;
using Food_Creator.Model.dto;
using Food_Creator.controller;
using FoodCreator.Tests.Shared;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FoodCreator.Tests.Ingredients.EndToEnd
{
    /// <summary>
    /// Pełen cykl testowy POST → PUT → DELETE składnika.
    /// </summary>
    public class Ingredients_EndToEndTests
    {
        [Fact]
        public async Task Ingredient_FullLifecycle_WorksCorrectly()
        {
            // Arrange
            var ctx = TestDbContextFactory.Create();
            var ctrl = new IngredientsController(ctx);

            // --- CREATE ---
            var dto = new IngredientDto { Name = "Cheese", Price = 5.0f };
            var post = await ctrl.CreateIngredient(dto);
            var created = Assert.IsType<CreatedAtActionResult>(post.Result);
            var entity = Assert.IsType<Ingredient>(created.Value);

            Assert.Equal("Cheese", entity.Name);
            Assert.Equal(5.0f, entity.Price);

            // --- UPDATE ---
            var update = new Ingredient
            {
                IngredientId = entity.IngredientId,
                Name = "Aged Cheese",
                Price = 6.0f
            };

            var put = await ctrl.UpdateIngredient(update);
            Assert.IsType<OkObjectResult>(put);

            var updatedEntity = await ctx.Ingredients.FindAsync(entity.IngredientId);
            Assert.Equal("Aged Cheese", updatedEntity!.Name);
            Assert.Equal(6.0f, updatedEntity.Price);

            // --- DELETE ---
            var del = await ctrl.DeleteIngredient("Aged Cheese");
            var deleted = Assert.IsType<OkObjectResult>(del.Result);
            Assert.Contains("deleted successfully", deleted.Value!.ToString());

            Assert.Empty(ctx.Ingredients);
        }
    }
}
