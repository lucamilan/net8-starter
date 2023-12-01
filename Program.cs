using sample.Components;
using sample.Components.Db;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped((sp) =>
{
    var handler = new HttpClientHandler
    {
        MaxConnectionsPerServer = 500,
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
    var baseUri = Environment.GetEnvironmentVariable("IS_DOCKER") != null ? "http://localhost" : "http://localhost:5070";
    return new HttpClient(handler){
        BaseAddress = new Uri(baseUri)
    };
});

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddHttpClient();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();



app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapGet("/api/db/create", async (AppDbContext dbContext) =>
{
    await dbContext.Database.EnsureCreatedAsync();

    return Results.Ok();
});

app.MapGet("/api/db/delete", async (AppDbContext dbContext) =>
{
    await dbContext.Database.EnsureDeletedAsync();

    return Results.Ok();
});

app.MapGet("/api/db/check", async (AppDbContext dbContext) =>
{
    var ok = await dbContext.Database.CanConnectAsync();

    return ok ? Results.Ok() : Results.Problem();
});

app.Run();
