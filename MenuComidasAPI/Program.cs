using Menu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Menu.Application.Services; // Ajusta el namespace según donde estén definidos los tipos
using Menu.Application.Interfaces;

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
