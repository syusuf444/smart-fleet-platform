using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Configure Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

// Enable CORS for React Frontend
builder.Services.AddCors(options => {
    options.AddPolicy("CorsPolicy", b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("CorsPolicy");

app.Map("/health", healthApp => healthApp.Run(async context =>
{
    await context.Response.WriteAsJsonAsync(new
    {
        status = "Healthy",
        service = "ApiGateway"
    });
}));

await app.UseOcelot();

app.Run();
