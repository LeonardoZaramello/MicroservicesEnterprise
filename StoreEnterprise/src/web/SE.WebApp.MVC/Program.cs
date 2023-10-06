using Microsoft.AspNetCore.Authentication.Cookies;
using Polly;
using Polly.Extensions.Http;
using SE.WebApp.MVC.Extensions;
using SE.WebApp.MVC.Services;
using SE.WebApp.MVC.Services.Handlers;

var builder = WebApplication.CreateBuilder(args);


//ENVIROMENT CONFIG
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath);
builder.Configuration.AddJsonFile("appsettings.json", true, true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);
builder.Configuration.AddEnvironmentVariables();
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<StartupBase>();
}

// DEPENDENCY INJECTION
builder.Services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
builder.Services.AddHttpClient<IAuthService, AuthService>();


builder.Services.AddHttpClient<ICatalogoService, CatalogoService>()
    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
    //.AddTransientHttpErrorPolicy( policy => policy.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));
    .AddPolicyHandler(PollyUtils.RetryPolicy())
    .AddTransientHttpErrorPolicy( policy => policy.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

#region Refit
//builder.Services.AddHttpClient("Refit",
//        options =>
//    {
//        options.BaseAddress = new Uri(builder.Configuration.GetSection("AppSettings").GetSection("CatalogoUrl").Value);
//    })
//    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
//    .AddTypedClient(Refit.RestService.For<ICatalogoServiceRefit>);
#endregion

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUser, AspNetUser>();

//APP SETTINGS
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

// Add services to the container.
// IDENTITY CONFIG
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/acess-denied";
    });


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
    pattern: "{controller=Catalogo}/{action=Index}/{id?}");

app.Run();
