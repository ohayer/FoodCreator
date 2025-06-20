public class IngredientDto
{
    public int IngredientID { get; set; }
    public string Name { get; set; }
    public string? Url { get; set; }
    public float Price { get; set; }
    public IFormFile? Image { get; set; }
}