using Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbProvider = builder.Configuration.GetValue<string>("DatabaseProvider");
var useExternalMigrations = builder.Configuration.GetValue<bool>("UseExternalMigrations");

// Configure database context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connStr = builder.Configuration.GetConnectionString("DefaultConnection");

    switch (dbProvider?.ToLower())
    {
        case "postgres":
            options.UseNpgsql(connStr);
            break;
        case "sqlserver":
            options.UseSqlServer(connStr);
            break;
        case "sqlite":
        default:
            options.UseSqlite(connStr);
            break;
    }
});

// Add controllers and OpenAPI
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var frontendOrigin = builder.Configuration.GetValue<string>("FrontendOrigin");

// ✅ Add CORS to allow the frontend to talk to the API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(frontendOrigin)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// ✅ Use CORS before anything that reads request headers
app.UseCors("AllowFrontend");

// Enable Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Run migrations and seeding if not using external scripts
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!useExternalMigrations)
    {
        db.Database.EnsureCreated();
        DbSeeder.Seed(db);
    }
}

app.Run();
