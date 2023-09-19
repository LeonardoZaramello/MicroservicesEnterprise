using SE.Core.Data;

namespace SE.Catalogo.API.Models
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> GetAll();
        Task<Produto> GetById(Guid id);

        void Add(Produto product);
        void Update(Produto product);
    }
}
