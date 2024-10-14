using Food_Creator;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var hotelsConnectionString = builder.Configuration.GetConnectionString("Food");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(hotelsConnectionString));

// Dodaje kontrolery
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
