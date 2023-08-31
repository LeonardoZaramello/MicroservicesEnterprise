using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace SE.Identity.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public abstract class MainController : Controller
    {
        public ICollection<string> Errors = new List<string>();


        protected IActionResult CustomResponse(object? result = null)
        {
            if (ValidOperation())
                return Ok(result);


            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Errors.ToArray() },
            }));
        }

        protected IActionResult CustomResponse(ModelStateDictionary modelState  )
        {
            var errors = modelState.Values.SelectMany(x => x.Errors);

            foreach (var error in errors)
            {
                Errors.Add(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected bool ValidOperation()
        {
            return Errors.Count == 0;
        }

        protected void AddError(string error)
        {
            Errors.Add(error);
        }

        // protected void CleanErrors()
        // {
        //     Errors.Clear();
        // }
    }
}