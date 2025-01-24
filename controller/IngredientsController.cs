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
    }
}