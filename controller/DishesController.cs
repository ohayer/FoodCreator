using Food_Creator.Model;
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
                IngredientUrl = di.Ingredient.Url,
                IngredientPrice = di.Ingredient.Price
            })
        });

        return Ok(result);
    }
    // POST: api/Dishes
[HttpPost]
public async Task<ActionResult<Dish>> CreateDish([FromBody] Dish newDish)
{
    if (newDish == null)
    {
        return BadRequest("Dish object cannot be null.");
    }

    if (string.IsNullOrWhiteSpace(newDish.Name))
    {
        return BadRequest("Dish name is required.");
    }

    try
    {
        // Utwórz nowe danie
        var dish = new Dish
        {
            Name = newDish.Name,
            Url = newDish.Url,
            Price = newDish.Price
        };

        // Jeśli są składniki, obsłuż ich dodanie
        if (newDish.DishIngredients != null && newDish.DishIngredients.Any())
        {
            foreach (var dishIngredient in newDish.DishIngredients)
            {
                var existingIngredient = await _context.Ingredients
                    .FirstOrDefaultAsync(i => i.Name == dishIngredient.Ingredient.Name);

                if (existingIngredient == null)
                {
                    // Dodanie nowego składnika do bazy danych
                    _context.Ingredients.Add(dishIngredient.Ingredient);
                    await _context.SaveChangesAsync();
                    existingIngredient = dishIngredient.Ingredient;
                }

                // Utworzenie relacji DishIngredient
                var newDishIngredient = new DishIngredient
                {
                    Dish = dish,
                    Ingredient = existingIngredient,
                    Quantity = dishIngredient.Quantity
                };

                _context.DishIngredients.Add(newDishIngredient);
            }
        }

        // Dodanie dania do bazy danych
        _context.Dishes.Add(dish);
        await _context.SaveChangesAsync();

        // Zwrócenie odpowiedzi 201 Created
        return CreatedAtAction(nameof(GetDishes), new { id = dish.DishId }, dish);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error creating dish: {ex.Message}");
        return StatusCode(500, "An error occurred while creating the dish.");
    }
}
}
    // TODO: Stworzyć metodę POST do tworzenia nowego dania
    // metoda ma zwracać poprawne kody wraz z prostym logowaniem wiadomości o błedach