using Food_Creator;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Pobranie connection string z appsettings.json
var connectionString = builder.Configuration.GetConnectionString("Food");

// Rejestracja ApplicationDbContext z connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


// Dodanie kontrolerów
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

var app = builder.Build();

// Automatyczne czyszczenie i migracja bazy danych
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    try
    {
        // Usunięcie bazy danych (jeśli istnieje)
        dbContext.Database.EnsureDeleted();

        // Stworzenie bazy danych i zastosowanie migracji
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Błąd podczas resetowania bazy danych: {ex.Message}");
        throw;
    }
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();