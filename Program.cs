using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;
using EventHub.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<EventHubContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("EventHubContext")
        ?? throw new InvalidOperationException(
            "Connection string 'EventHubContext' not found.")));

var mongoConnectionString = builder.Configuration["MongoDb:ConnectionString"]
    ?? throw new InvalidOperationException(
        "MongoDB connection string not found.");

builder.Services.AddSingleton<IMongoClient>(
    _ => new MongoClient(mongoConnectionString));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();