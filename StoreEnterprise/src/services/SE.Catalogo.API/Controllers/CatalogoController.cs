using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SE.Catalogo.API.Models;
using SE.WebAPI.Core.Identidade;

namespace SE.Catalogo.API.Controllers
{
    [ApiController]
    [Authorize]
    public class CatalogoController : Controller
    {
        private readonly IProdutoRepository _productRepository;

        public CatalogoController(IProdutoRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [AllowAnonymous]
        [HttpGet("catalogo/produtos")]
        public async Task<IEnumerable<Produto>> GetProducts()
        {
            return await _productRepository.GetAll();
        }

        [ClaimsAuthorize("Catalogo", "Ler")]
        [HttpGet("catalogo/produtos/{id}")]
        public async Task<Produto> GetProductById(Guid id)
        {
            throw new Exception("Erro!");

            return await _productRepository.GetById(id);
        }
    }
}
