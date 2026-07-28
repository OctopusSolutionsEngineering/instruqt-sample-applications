using Microsoft.Extensions.Options;
using OpenFeature;
using OpenFeature.Hosting;
using OpenFeature.Hosting.Providers.Memory;

var builder = WebApplication.CreateBuilder(args);

// Bind configuration to FrontendConfig and register for DI
builder.Services.Configure<FrontendConfig>(builder.Configuration);

// Register OpenFeature — no AddHostedFeatureLifecycle() needed anymore
builder.Services.AddOpenFeature(featureBuilder =>
{
    featureBuilder
        .AddInMemoryProvider(); // swap for your real provider
});

// Add Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();

// Simple POCO config class
public class FrontendConfig
{
    public string ApiBaseUrl { get; set; } = string.Empty;
}
