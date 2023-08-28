using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SE.Identidade.API.Controllers
{
    [ApiController]
    [Route("test")]
    public class HelloWorld : Controller
    {

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> OlarMundo()
        {
            var user = HttpContext.User as ClaimsPrincipal;

            return Ok($"hello {user.Identity!.Name}");
        }
    }
}
