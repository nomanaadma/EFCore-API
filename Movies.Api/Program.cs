using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Movies.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());	
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MoviesContext>();

var app = builder.Build();


// Temporary solution, need to fix later
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<MoviesContext>();

var pendingMigration = await context.Database.GetPendingMigrationsAsync();

if (pendingMigration.Any())
	throw new Exception("Database is not fully migrated for MoviesContext");

// await context.Database.MigrateAsync();
// context.Database.EnsureDeleted();
// context.Database.EnsureCreated();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();