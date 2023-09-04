using Microsoft.AspNetCore.Mvc;
using SE.WebApp.MVC.Models;

namespace SE.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponseContainErrors(ResponseResult responseResult)
        {
            if(responseResult != null && responseResult.Errors.Messages.Count != 0)
            {
                foreach (var message in responseResult.Errors.Messages)
                {
                    ModelState.AddModelError(string.Empty, message);
                }
                return true;
            }

            return false;
        }
    }
}
