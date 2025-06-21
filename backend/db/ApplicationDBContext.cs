using Food_Creator.Model;
using Microsoft.EntityFrameworkCore;

namespace Food_Creator;


public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<Dish>           Dishes          => Set<Dish>();
    public DbSet<Ingredient>     Ingredients     => Set<Ingredient>();
    public DbSet<DishIngredient> DishIngredients => Set<DishIngredient>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Klucz złożony dla tabeli łączącej
        modelBuilder.Entity<DishIngredient>()
            .HasKey(di => new { di.DishId, di.IngredientId });

        // Seed – dane z osobnej klasy
        modelBuilder.Entity<Ingredient>().HasData(DbSeeds.Ingredients);
        modelBuilder.Entity<Dish>()      .HasData(DbSeeds.Dishes);
        modelBuilder.Entity<DishIngredient>()
            .HasData(DbSeeds.DishIngredients);
    }
}