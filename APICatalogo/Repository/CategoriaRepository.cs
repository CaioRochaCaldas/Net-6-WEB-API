using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<Categoria> GetCategoriasPaginas(CategoriasParameters categoriaParameters)
        {
            return PagedList<Categoria>.ToPagedList(Get().OrderBy(on => on.Nome),categoriaParameters.PageNumber,
                categoriaParameters.PageSize);
        } //implementação metodo de paginação

        //metodo especifico implementado aqui
        public async Task<IEnumerable<Categoria>> GetCategoriasProdutos() //Agora é async
        {
            return await Get().Include(x=>x.Produtos).ToListAsync();
        }
    }
}
