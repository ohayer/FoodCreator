using CsvHelper;
using CsvHelper.TypeConversion;
using Food_Creator.BusinessLogic;
using Xunit;

public class CsvIngredientParserTests
{
    /// <summary>
    /// Poprawny CSV zwraca dwa rekordy.
    /// </summary>
    [Fact]
    public void Parse_ValidCsv_ReturnsTwoIngredients()
    {
        var csv = "Id,Name,Price\n1,Salt,0.3\n2,Sugar,1.0";
        var list = CsvIngredientParser.Parse(csv);

        Assert.Equal(2, list.Count);
        Assert.Equal("Salt", list[0].Name);
    }

    /// <summary>
    /// Brak kolumny <c>Price</c> → CsvHelper.HeaderValidationException.
    /// </summary>
    [Fact]
    public void Parse_MissingColumn_Throws()
    {
        var bad = "Id,Name\n1,Salt";
        Assert.Throws<HeaderValidationException>(() => CsvIngredientParser.Parse(bad));
    }

    /// <summary>
    /// Niepoprawna liczba w kolumnie <c>Price</c> → CsvHelper.TypeConverterException.
    /// </summary>
    [Fact]
    public void Parse_InvalidPrice_Throws()
    {
        var bad = "Id,Name,Price\n1,Salt,abc";
        Assert.Throws<TypeConverterException>(() => CsvIngredientParser.Parse(bad));
    }
}