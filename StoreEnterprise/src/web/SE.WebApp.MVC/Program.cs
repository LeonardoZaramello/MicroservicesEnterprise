using Microsoft.AspNetCore.Authentication.Cookies;
using SE.WebApp.MVC.Extensions;
using SE.WebApp.MVC.Services;

var builder = WebApplication.CreateBuilder(args);

//APP SETTINGS
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

//ENVIROMENT CONFIG
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath);
builder.Configuration.AddJsonFile("appsettings.json", true, true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);
builder.Configuration.AddEnvironmentVariables();
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<StartupBase>();
}

// Add services to the container.
// IDENTITY CONFIG
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/acess-denied";
    });

// DEPENDENCY INJECTION
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUser, AspNetUser>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error/500");
    app.UseStatusCodePagesWithRedirects("/error/{0}");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//MIDDLEWARES
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
