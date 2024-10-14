namespace Food_Creator.Model;

public class Dish
{
    public int DishId { get; set; }
    public Food Food { get; set; }
    public int FoodId { get; set; }
    public List<DishIngredient> DishIngredients { get; set; } = new();
}