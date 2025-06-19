using Food_Creator.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Food_Creator.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Konstruktor wstrzykujcy ApplicationDbContext (DbContext)
        public IngredientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients()
        {
            // Zwraca wszystkie ingredients z bazy danych
            var ingredients = await _context.Ingredients.ToListAsync();
            return Ok(ingredients);
        }

        [HttpDelete]
        public async Task<ActionResult<Ingredient>> DeleteIngredient([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new { Message = "Ingredient name is required" });
            }

            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(x => x.Name == name);

            if (ingredient == null)
            {
                return NotFound(new { Message = $"Ingredient {name} does not exist" });
            }

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Ingredient {ingredient.Name} deleted successfully" });
        }

        [HttpPut]
        public async Task<ActionResult> UpdateIngredient([FromBody] Ingredient updateIngredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid data" });
            }

            var ingredientToUpdate = await _context.Ingredients.FindAsync(updateIngredient.IngredientId);

            if (ingredientToUpdate == null)
            {
                return NotFound(new { Message = $"Ingredient with {updateIngredient.IngredientId} does not exist" });
            }

            ingredientToUpdate.Name = updateIngredient.Name;
            ingredientToUpdate.Url = updateIngredient.Url;
            ingredientToUpdate.Price = updateIngredient.Price;

            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Ingredient {ingredientToUpdate.IngredientId} updated successfully" });
        }
        
        [HttpPost]
        public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] Ingredient newIngredient)
        {
            if (newIngredient == null || string.IsNullOrWhiteSpace(newIngredient.Name))
            {
                return BadRequest(new { Message = "Invalid ingredient data" });
            }

            try
            {
                var ingredient = new Ingredient
                {
                    Name = newIngredient.Name,
                    Url = newIngredient.Url,
                    Price = newIngredient.Price
                };


                _context.Ingredients.Add(ingredient);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetIngredients), new { ingredientId = ingredient.IngredientId },
                    ingredient);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error creating ingredient: {ex.Message}");
                return BadRequest(newIngredient);
            }

        }
    }
}