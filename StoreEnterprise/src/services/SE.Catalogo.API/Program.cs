using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SE.Catalogo.API.Data;
using SE.Catalogo.API.Data.Repository;
using SE.Catalogo.API.Models;
using SE.WebAPI.Core.Identidade;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// DEPENDENCY INJECTION
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<CatalogoContext>();

//DBCONTEXT
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CatalogoContext>(options => options.UseSqlServer(connectionString));

//ENVIROMENT CONFIG
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath);
builder.Configuration.AddJsonFile("appsettings.json", true, true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);
builder.Configuration.AddEnvironmentVariables();
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<StartupBase>();
}

// ADDJWT
builder.Services.AddJwtConfiguration(builder.Configuration);

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Total", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// SWAGGER DOCS
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Store Enterprise Catalogo API",
        Description = "Esta API faz parte do curso ASP.NET Core Enterprise Applications",
        Contact = new OpenApiContact() { Name = "Leonardo Zaramello", Email = "leonardogfz@hotmail.com" },
        License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
    });
});



//APPCONFIG
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Total");

app.UseAuthConfiguration();

app.MapControllers();

app.Run();
