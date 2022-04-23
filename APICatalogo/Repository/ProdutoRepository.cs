using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository
{
 // ProdutoRepository (Implementa metodos expecificos) <- IProductRepository <-  <- Repository ( implementa ) <- IRepository  
    public class ProdutoRepository : Repository<Produto> ,IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context)
        {
            //contexto
        }
        
        //implementação metodo de paginação
        public PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters)//recebe como parametro a regra de negocio de paginação novamente
        {
            // metodos de coisas que vamos lidar
            /* return Get().OrderBy(on => on.Nome)
                 .Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize) //bula toda a informação desnecessaria e vai direto ao fim da pagina
                 .Take(produtosParameters.PageSize)
                 .ToList();*/

            //agora o retorno é desse tipo de classe
            return PagedList<Produto>.ToPagedList(Get().OrderBy(on => on.ProdutoId), produtosParameters.PageNumber, produtosParameters.PageSize);

        }

        public async Task<IEnumerable<Produto>> GetProdutosPorPreco() //agora é async
        {
            //metodo de especifico de Produtos de pegar produtos por preco
            return await Get().OrderBy(c => c.Preco).ToListAsync();
        }
    }
}
