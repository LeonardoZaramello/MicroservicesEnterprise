using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SE.Identidade.API.data;
using SE.Identidade.API.Models;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// USER SCHEMA
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();


//DBCONTEXT
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

//IDENTITY
builder.Services.AddIdentityCore<UserRegister>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddApiEndpoints();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

// MAPPING USER TO API
app.MapIdentityApi<UserRegister>();



app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
