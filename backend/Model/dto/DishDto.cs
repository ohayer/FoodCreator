namespace Food_Creator.Model.dto;

public class DishDto
{
    public string Name { get; set; }
    public float Price { get; set; }
    public List<DishIngredientDto> DishIngredients { get; set; }
}