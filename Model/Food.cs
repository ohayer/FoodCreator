namespace Food_Creator.Model;

public class Food
{
    public int FoodId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public float Price { get; set; }
    public List<Ingredient> Ingredients { get; set; } = new();
}