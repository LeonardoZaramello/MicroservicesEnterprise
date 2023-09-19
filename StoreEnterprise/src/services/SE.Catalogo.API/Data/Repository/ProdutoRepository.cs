using Microsoft.EntityFrameworkCore;
using SE.Catalogo.API.Models;
using SE.Core.Data;

namespace SE.Catalogo.API.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {

        private readonly CatalogoContext _context;

        public ProdutoRepository(CatalogoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }

        public async Task<Produto> GetById(Guid id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public void Add(Produto product)
        {
            _context.Produtos.Add(product);
        }

        public void Update(Produto product)
        {
            _context.Produtos.Update(product);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
