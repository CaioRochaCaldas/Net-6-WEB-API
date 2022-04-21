using APICatalogo.Context;
using APICatalogo.Models;

namespace APICatalogo.Repository
{
 // ProdutoRepository (Implementa metodos expecificos) <- IProductRepository <-  <- Repository ( implementa ) <- IRepository  
    public class ProdutoRepository : Repository<Produto> ,IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context)
        {
            //contexto
        }

        public IEnumerable<Produto> GetProdutosPorPreco()
        {
            //metodo de especifico de Produtos de pegar produtos por preco
            return Get().OrderBy(c => c.Preco).ToList();
        }
    }
}
