using Microsoft.EntityFrameworkCore;
using SE.Catalogo.API.Data;
using SE.Catalogo.API.Data.Repository;
using SE.Catalogo.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//DBCONTEXT
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CatalogoContext>(options => options.UseSqlServer(connectionString));

// DEPENDENCY INJECTION
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<CatalogoContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
