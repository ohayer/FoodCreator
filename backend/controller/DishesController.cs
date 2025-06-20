using Food_Creator.Model;
using Food_Creator.Model.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Food_Creator.controller;

[ApiController]
[Route("api/[controller]")]
public class DishesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    // Konstruktor wstrzykujcy ApplicationDbContext (DbContext)
    public DishesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Dishes
    [HttpGet]
    public async Task<IActionResult> GetDishes()
    {
        var dishes = await _context.Dishes
            .Include(d => d.DishIngredients)
            .ThenInclude(di => di.Ingredient)
            .ToListAsync();

        var result = dishes.Select(d => new
        {
            DishName = d.Name,
            Ingredients = d.DishIngredients.Select(di => new
            {
                IngredientName = di.Ingredient.Name,
                Quantity = di.Quantity,
                IngredientPrice = di.Ingredient.Price
            })
        });

        return Ok(result);
    }
    // POST: api/Dishes
[HttpPost]
[HttpPost]
public async Task<ActionResult<Dish>> CreateDish([FromBody] DishDto newDishDto)
{
    if (newDishDto == null)
    {
        return BadRequest("Dish object cannot be null.");
    }

    if (string.IsNullOrWhiteSpace(newDishDto.Name))
    {
        return BadRequest("Dish name is required.");
    }

    try
    {
        // Utwórz nowe danie
        var dish = new Dish
        {
            Name = newDishDto.Name,
            Price = newDishDto.Price
        };

        // Obsłuż składniki
        if (newDishDto.DishIngredients != null && newDishDto.DishIngredients.Any())
        {
            foreach (var dishIngredientDto in newDishDto.DishIngredients)
            {
                var existingIngredient = await _context.Ingredients
                    .FirstOrDefaultAsync(i => i.IngredientId == dishIngredientDto.IngredientId);

                if (existingIngredient == null)
                {
                    throw new Exception($"Ingredient with ID {dishIngredientDto.IngredientId} does not exist.");
                }

                var newDishIngredient = new DishIngredient
                {
                    Dish = dish,
                    Ingredient = existingIngredient,
                    Quantity = dishIngredientDto.Quantity
                };

                _context.DishIngredients.Add(newDishIngredient);
            }
        }

        // Dodaj danie do bazy
        _context.Dishes.Add(dish);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDishes), new { id = dish.DishId }, dish);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error creating dish: {ex.Message}");
        return StatusCode(500, "An error occurred while creating the dish.");
    }
}

}