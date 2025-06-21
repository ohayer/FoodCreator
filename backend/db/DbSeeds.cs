using Food_Creator.Model;

namespace Food_Creator;

public static class DbSeeds
{

    public static readonly Ingredient[] Ingredients =
    [
        new() { IngredientId = 1, Name = "Tomato",    Price = 0.50f },
        new() { IngredientId = 2, Name = "Cheese",    Price = 1.50f },
        new() { IngredientId = 3, Name = "Basil",     Price = 0.20f },
        new() { IngredientId = 4, Name = "Beef 100g", Price = 5.00f },
        new() { IngredientId = 5, Name = "Bacon 30g", Price = 2.88f }
    ];

    public static readonly Dish[] Dishes =
    [
        new() { DishId = 1, Name = "Pizza",  Price = 9.99f },
        new() { DishId = 2, Name = "Burger", Price = 5.99f }
    ];

    public static readonly DishIngredient[] DishIngredients =
    [
        new() { DishId = 1, IngredientId = 1, Quantity = 2 },
        new() { DishId = 1, IngredientId = 2, Quantity = 1 },
        new() { DishId = 1, IngredientId = 3, Quantity = 3 },
        new() { DishId = 2, IngredientId = 1, Quantity = 4 },
        new() { DishId = 2, IngredientId = 4, Quantity = 2 },
        new() { DishId = 2, IngredientId = 5, Quantity = 1 }
    ];
}