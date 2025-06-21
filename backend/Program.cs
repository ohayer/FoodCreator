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
    .AddApplicationPart(typeof(Food_Creator.controller.IngredientsController).Assembly)
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;

        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (!app.Environment.IsEnvironment("Testing"))
{
// Automatyczne czyszczenie i migracja bazy danych dla środowiska nietestowego
// Migracja bazy danych przy starcie (bez kasowania danych!)
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            // Tylko stosuje migracje — NIE USUWA bazy!
            dbContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas migracji bazy danych: {ex.Message}");
            throw;
        }

        dbContext.Database.EnsureCreated();
    }
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowAll");
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "Food Creator API is running ");
app.Run();