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
    public async Task<ActionResult<IEnumerable<Dish>>> GetDishes()
    {
        // Zwraca wszystkie dania z bazy danych
        // TODO: zaimplementować logikę aby ta metoda również zwracała składniki dania
        // najlepiej przenieść tę logikę do klasy serwisu o nazwie getDishesWithIngredients
        var dishes = await _context.Dishes.ToListAsync();
        return Ok(dishes);
    }
    
    // TODO: Stworzyć metodę POST do tworzenia nowego dania
    // metoda ma zwracać poprawne kody wraz z prostym logowaniem wiadomości o błedach
}