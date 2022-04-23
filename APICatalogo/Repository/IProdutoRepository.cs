using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        //metodos especifico de produto

        // IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParameters);//faz paginação recebendo a regra de paginação

        Task< PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters);//agora o retorna da paginação vai ser page de list pois mudamos.
        Task<IEnumerable<Produto>> GetProdutosPorPreco(); //lista de todos os produtos por preço
    }
}
