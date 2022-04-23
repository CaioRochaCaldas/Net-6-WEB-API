using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<PagedList<Categoria>> GetCategoriasPaginas(CategoriasParameters categoriaParameters);//metodo de paginação
        Task<IEnumerable<Categoria>> GetCategoriasProdutos(); //Transformamos em async 
    }
}
