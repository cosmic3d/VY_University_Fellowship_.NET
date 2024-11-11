using PokeApiDDD.Infrastructure.Contracts;
using PokeApiDDD.Infrastructure.Impl.DB;
using PokeApiDDD.Infrastructure.Impl.JSON;
using PokeApiDDD.Library.Contracts;
using PokeApiDDD.Library.Impl;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
builder.Services
    .AddScoped<IPokemonService, PokemonService>()
    .AddScoped<IPokemonRepositoryDB, PokemonRepositoryDB>()
    .AddScoped<IPokemonRepositoryJSON, PokemonRepositoryJSON>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
