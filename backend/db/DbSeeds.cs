using Food_Creator.Model;

namespace Food_Creator;

public class DbSeeds
{
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>
    {
        new Ingredient { IngredientId = 1, Name = "Tomato", Price = 0.50f },
        new Ingredient { IngredientId = 2, Name = "Cheese", Price = 1.50f },
        new Ingredient { IngredientId = 3, Name = "Basil", Price = 0.20f },
        new Ingredient { IngredientId = 4, Name = "Beef 100g", Price = 5f },
        new Ingredient { IngredientId = 5, Name = "Bacon 30g", Price = 2.88f }
    };

    public List<Dish> Dishes { get; set; } = new List<Dish>
    {
        new Dish { DishId = 1, Name = "Pizza", Price = 9.99f },
        new Dish { DishId = 2, Name = "Burger", Price = 5.99f }
    };

    public List<DishIngredient> DishIngredients { get; set; } = new List<DishIngredient>
    {
        new DishIngredient { DishId = 1, IngredientId = 1, Quantity = 2 },
        new DishIngredient { DishId = 1, IngredientId = 2, Quantity = 1 },
        new DishIngredient { DishId = 1, IngredientId = 3, Quantity = 3 },
        new DishIngredient { DishId = 2, IngredientId = 1, Quantity = 4 },
        new DishIngredient { DishId = 2, IngredientId = 4, Quantity = 2 },
        new DishIngredient { DishId = 2, IngredientId = 5, Quantity = 1 }
    };
}