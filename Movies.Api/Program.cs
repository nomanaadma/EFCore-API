using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Movies.Api.Data;
using Movies.Api.Repositories;
using Movies.Api.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IGenreRepository, GenreRepository>();
builder.Services.AddTransient<IBatchGenreService, BatchGenreService>();
builder.Services.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());	
});

var serilog = new LoggerConfiguration()
	.ReadFrom.Configuration(builder.Configuration)
	.CreateLogger();

builder.Services.AddSerilog(serilog);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MoviesContext>(optionBuilder =>
{
	var connectionString = builder.Configuration.GetConnectionString("MoviesContext");
	optionBuilder
		.UseSqlServer(connectionString, sqlBuilder => sqlBuilder.MaxBatchSize(50))
		.LogTo(Console.WriteLine);

}, ServiceLifetime.Scoped, ServiceLifetime.Singleton);

var app = builder.Build();


// Temporary solution, need to fix later
using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<MoviesContext>();
	var pendingMigration = await context.Database.GetPendingMigrationsAsync();
	if (pendingMigration.Any())
		throw new Exception("Database is not fully migrated for MoviesContext");
};


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