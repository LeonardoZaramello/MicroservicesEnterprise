using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SE.WebApp.MVC.Models;
using SE.WebApp.MVC.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SE.WebApp.MVC.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IAuthService _authService;

        public IdentityController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [Route("/nova-conta")]
        public IActionResult Register()
        {
            return View(); 
        }

        [HttpPost]
        [Route("/nova-conta")]
        public async Task<IActionResult> Register(UserRegister userRegister)
        {
            if(!ModelState.IsValid) return View(userRegister);

            var response = await _authService.Register(userRegister);

            await PerformLogin(response);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid) return View(userLogin);

            var response = await _authService.Login(userLogin);

            await PerformLogin(response);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            return RedirectToAction("Index", "Home");
        }

        [NonAction]
        private async Task PerformLogin(UserResponseLogin userResponseLogin)
        {
            var token = new JwtSecurityTokenHandler().ReadToken(userResponseLogin.AcessToken) as JwtSecurityToken;

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", userResponseLogin.AcessToken));
            claims.AddRange(token!.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(5),
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );
        }

        //private static JwtSecurityToken GetFormatedToken(string jwtToken)
        //{
        //    return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
        //}
    }
}
