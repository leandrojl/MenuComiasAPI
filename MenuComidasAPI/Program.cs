using Menu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Menu.Application.Services; // Ajusta el namespace seg�n donde est�n definidos los tipos
using Menu.Application.Interfaces;
using Menu.Domain.Interfaces;
using Menu.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MenuDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MenuDatabase")));
builder.Services.AddScoped<ITipoComidaService, TipoComidaService>();
builder.Services.AddScoped<IIngredienteService, IngredienteService>();
builder.Services.AddScoped<IComidaService, ComidaService>();
builder.Services.AddScoped<IComidaIngredienteService, ComidaIngredienteService>();
builder.Services.AddScoped<ITipoComidaRepository, TipoComidaRepository>();
builder.Services.AddScoped<IComidaRepository, ComidaRepository>();
builder.Services.AddScoped<IIngredienteRepository, IngredienteRepository>();
builder.Services.AddScoped<IComidaIngredienteRepository, ComidaIngredienteRepository>();


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
