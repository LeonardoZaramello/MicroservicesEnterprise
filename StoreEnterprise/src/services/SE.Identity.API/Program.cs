using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SE.Identity.API;
using SE.Identity.API.data;
using SE.WebAPI.Core.Identidade;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//ENVIROMENT CONFIG
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath);
builder.Configuration.AddJsonFile("appsettings.json", true, true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);
builder.Configuration.AddEnvironmentVariables();
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<StartupBase>();
}

//DBCONTEXT
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

//IDENTITY CONFIG
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddErrorDescriber<IdentityPtBrMessages>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddJwtConfiguration(builder.Configuration);

// JWT - ENV VARIABLES
//var appSettingsSection = builder.Configuration.GetSection("AppSettings");
//builder.Services.Configure<AppSettings>(appSettingsSection);
//var appSettings = appSettingsSection.Get<AppSettings>();
//var key = Encoding.ASCII.GetBytes(appSettings.Secret);

// JWT - CONFIG
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(bearerOptions =>
//{
//    bearerOptions.RequireHttpsMetadata = true;
//    bearerOptions.SaveToken = true;
//    bearerOptions.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidAudience = appSettings.ValidoEm,
//        ValidIssuer = appSettings.Emissor
//    };
//});

// API CONFIG
builder.Services.AddControllers();

// SWAGGER DOCS
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Store Enterprise",
        Description = "Esta API faz parte do curso ASP.NET Core Enterprise Applications",
        Contact = new OpenApiContact() { Name = "Leonardo Zaramello", Email = "leonardogfz@hotmail.com" },
        License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();
app.UseAuthConfiguration();

app.MapControllers();

app.Run();
