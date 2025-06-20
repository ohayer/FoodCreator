using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Food_Creator.Model;

namespace Food_Creator.BusinessLogic;

public static class CsvIngredientParser
{
    private sealed class Map : ClassMap<Ingredient>
    {
        public Map()
        {
            Map(m => m.IngredientId).Name("Id");
            Map(m => m.Name);
            Map(m => m.Price);
        }
    }

    /// <summary>
    /// Parsuje CSV (nagłówki: Id,Name,Price) i zwraca listę Ingredient.
    /// Rzuca FormatException, jeżeli format liczby jest niepoprawny.
    /// Rzuca CsvHelperException, jeżeli brakuje kolumn.
    /// </summary>
    public static IReadOnlyList<Ingredient> Parse(string csv)
    {
        using var reader = new StringReader(csv);
        using var csvR = new CsvReader(reader, CultureInfo.InvariantCulture);
        csvR.Context.RegisterClassMap<Map>();

        return csvR.GetRecords<Ingredient>().ToList();
    }
}