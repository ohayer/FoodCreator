using Food_Creator.Model;

namespace Food_Creator;

public class DbSeeds
{
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>
    {
        new Ingredient { IngredientId = 1, Name = "Tomato", Price = 0.50f, Url = "https://5.imimg.com/data5/RU/NM/CJ/SELLER-27229577/tomato-500x500.jpg" },
        new Ingredient { IngredientId = 2, Name = "Cheese", Price = 1.50f, Url = "https://sklep.spolemkielce.pl/wp-content/uploads/2024/06/ser-z-oczkiem-wloszczowa.jpg" },
        new Ingredient { IngredientId = 3, Name = "Basil", Price = 0.20f, Url = "https://p.fide.pl/1600/7/8/78303_veritable_podstawowe_ziola_wklad_nasienny_bazylia.jpg" },
        new Ingredient { IngredientId = 4, Name = "Beef 100g", Price = 5f, Url = "https://smakigarwolina.pl/wp-content/uploads/2017/06/wolowina-bez-kosci.jpg" },
        new Ingredient { IngredientId = 5, Name = "Bacon 30g", Price = 2.88f, Url = "https://media.istockphoto.com/id/508755080/pl/zdj%C4%99cie/gotowane-plasterki-bekonu-zbli%C5%BCenie-odizolowane-na-na-bia%C5%82e-t%C5%82o.jpg?s=612x612&w=0&k=20&c=SSJBEO0G3F4r83gYWe6zYaKq349RB4J8QyONEyzgJ7c=" }
    };

    public List<Dish> Dishes { get; set; } = new List<Dish>
    {
        new Dish { DishId = 1, Name = "Pizza", Url = "https://pizzapozachodzie.pl/wp-content/uploads/2021/10/pizzeria-template-header-pizza-img.png", Price = 9.99f },
        new Dish { DishId = 2, Name = "Burger", Url = "https://www.maxpremiumburgers.pl/globalassets/images/pl-menu/burgery/2024/original_noketchup_singel.jpg?width=1160&sharpen=5&sigma=1,4&threshold=0", Price = 5.99f }
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