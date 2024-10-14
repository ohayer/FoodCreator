using Food_Creator.Model;

namespace Food_Creator;

public class DbSeeds
{
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>
    {
        new Ingredient { IngredientId = 1, Name = "Tomato", Price = 0.50f, Type = IngredientTypes.Vegetable, Url = "http://example.com/tomato" },
        new Ingredient { IngredientId = 2, Name = "Cheese", Price = 1.50f, Type = IngredientTypes.Dairy, Url = "http://example.com/cheese" },
        new Ingredient { IngredientId = 3, Name = "Basil", Price = 0.20f, Type = IngredientTypes.Spice, Url = "http://example.com/basil" },
        new Ingredient { IngredientId = 4, Name = "Beef 100g", Price = 5f, Type = IngredientTypes.Meat, Url = "http://example.com/beef" },
        new Ingredient { IngredientId = 5, Name = "Bacon 30g", Price = 2.88f, Type = IngredientTypes.Meat, Url = "http://example.com/bacon" }
    };


    public List<Food> Foods { get; set; } = new List<Food>
    {
        new Food { FoodId = 1, Name = "Pizza", Url = "http://example.com/pizza", Price = 9.99f },
        new Food { FoodId = 2, Name = "Burger", Url = "http://example.com/burger", Price = 5.99f }
    };

    public List<Dish> Dishes { get; set; } = new List<Dish>
    {
        new Dish { DishId = 1, FoodId = 1 },
        new Dish { DishId = 2, FoodId = 2 }
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