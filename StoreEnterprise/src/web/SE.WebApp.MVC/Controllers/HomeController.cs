using Microsoft.AspNetCore.Mvc;
using SE.WebApp.MVC.Models;
using System.Diagnostics;

namespace SE.WebApp.MVC.Controllers
{
    public class HomeController : MainController
    {
        [Route("sistema-indisponivel")]
        public IActionResult SistemaIndisponivel()
        {
            var modelErro = new ErrorViewModel
            {
                Message = "O sistema est� temporariamente indispon�vel, isto pode ocorrer em momentos de sobrecarga de usu�rios.",
                Title = "Sistema indispon�vel.",
                ErrorCode = 500
            };

            return View("Error", modelErro);
        }

        [Route("error/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel();

            if (id == 500)
            {
                modelErro.Message = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelErro.Title = "Ocorreu um erro!";
                modelErro.ErrorCode = id;
            }
            else if (id == 404)
            {
                modelErro.Message =
                    "A p�gina que est� procurando n�o existe! <br />Em caso de d�vidas entre em contato com nosso suporte";
                modelErro.Title = "Ops! P�gina n�o encontrada.";
                modelErro.ErrorCode = id;
            }
            else if (id == 403)
            {
                modelErro.Message = "Voc� n�o tem permiss�o para fazer isto.";
                modelErro.Title = "Acesso Negado";
                modelErro.ErrorCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", modelErro);
        }
    }
}
