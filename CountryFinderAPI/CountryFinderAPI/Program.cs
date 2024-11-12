using CountryFinderAPI.Infrastructure.Contracts;
using CountryFinderAPI.Infrastructure.Impl;
using CountryFinderAPI.Library.Contracts;
using CountryFinderAPI.Library.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ICountryFinderRepository, CountryFinderRepository>();
builder.Services.AddScoped<ICountryFinderService, CountryFinderService>();
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
