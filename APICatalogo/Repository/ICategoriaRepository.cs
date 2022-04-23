using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        PagedList<Categoria> GetCategoriasPaginas(CategoriasParameters categoriaParameters);//metodo de paginação
        IEnumerable<Categoria> GetCategoriasProdutos();
    }
}
