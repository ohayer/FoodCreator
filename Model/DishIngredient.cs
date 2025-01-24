namespace Food_Creator.Model;

public class DishIngredient
{
    public int DishId { get; set; }
    public Dish Dish { get; set; }   // Relacja do Dish
    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; }   // Relacja do Ingredient
    public float Quantity { get; set; }
}