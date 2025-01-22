namespace Food_Creator.Model;

public class Ingredient
{
    public int IngredientId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public float Price { get; set; }
    public IngredientTypes Type { get; set; }
    public List<DishIngredient> DishIngredients { get; set; } = new();
}