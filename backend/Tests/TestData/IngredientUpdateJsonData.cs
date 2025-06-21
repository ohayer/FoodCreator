using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Food_Creator.Model;
using Food_Creator;

public class IngredientUpdateJsonData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var json = File.ReadAllText("TestData/ingredient_updates.json");
        var ingredients = JsonSerializer.Deserialize<List<Ingredient>>(json)!;

        foreach (var ingredient in ingredients)
        {
            yield return new object[] { ingredient.Name, ingredient.Price };
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
