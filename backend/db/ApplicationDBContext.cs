using Food_Creator.Model;
using Microsoft.EntityFrameworkCore;

namespace Food_Creator
{
    public class ApplicationDbContext : DbContext
    {
        // Konstruktor przyjmujący opcje kontekstu
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet reprezentujące tabele w bazie danych
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }

        // Metoda konfigurująca model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definiowanie klucza złożonego dla tabeli DishIngredient (tabela łącząca)
            modelBuilder.Entity<DishIngredient>()
                .HasKey(di => new { di.DishId, di.IngredientId });

            // Dodawanie danych początkowych przy tworzeniu bazy danych
            var dbSeeds = new DbSeeds();
            modelBuilder.Entity<Ingredient>().HasData(
                dbSeeds.Ingredients);
            modelBuilder.Entity<Dish>().HasData(
                dbSeeds.Dishes);
            modelBuilder.Entity<DishIngredient>().HasData(
                dbSeeds.DishIngredients);
            // Dodatkowe konfiguracje mogą iść tutaj
        }
    }
}