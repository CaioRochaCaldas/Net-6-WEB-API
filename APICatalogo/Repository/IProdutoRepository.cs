using APICatalogo.Models;

namespace APICatalogo.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        //metodo especifico de produto
        IEnumerable<Produto> GetProdutosPorPreco(); //lista de todos os produtos por preço
    }
}
