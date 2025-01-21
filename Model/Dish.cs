namespace Food_Creator.Model;

public class Dish
{
    public int DishId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public float Price { get; set; }
    public List<Ingredient> Ingredients { get; set; } = new();
}