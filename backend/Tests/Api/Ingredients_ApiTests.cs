using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

public class Ingredients_ApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public Ingredients_ApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }
    /// <summary>
    /// Testy API - Zwraca 200 OK oraz JSON z listą składników, gdy dane istnieją
    /// </summary>
    [Fact]
    public async Task GetIngredients_ReturnsOkAndJson()
    {
        // Act
        var response = await _client.GetAsync("/api/ingredients");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("Salt", content);
        Assert.Contains("Sugar", content);
    }
    /// <summary>
    /// Testy HTTP - Wysłanie poprawnego formularza z obrazkiem zwraca 201 Created i tworzy składnik
    /// </summary>
    [Fact]
    public async Task PostIngredient_WithImage_ReturnsCreated()
    {
        // Arrange
        var imageBytes = new byte[] { 1, 2, 3, 4, 5 };
        var imageContent = new ByteArrayContent(imageBytes);
        imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

        var form = new MultipartFormDataContent
        {
            { new StringContent("Paprika"), "Name" },
            { new StringContent("0,99"), "Price" },
            { imageContent, "Image", "paprika.jpg" }
        };

        // Act
        var response = await _client.PostAsync("/api/ingredients", form);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("Paprika", content);
    }
}
