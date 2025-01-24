namespace Food_Creator.Model;

public class Ingredient
{
    public int IngredientId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public float Price { get; set; }
    public ICollection<DishIngredient> DishIngredients { get; set; }
}