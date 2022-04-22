using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        //metodos especifico de produto

        IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParameters);//faz paginação recebendo a regra de paginação
        IEnumerable<Produto> GetProdutosPorPreco(); //lista de todos os produtos por preço
    }
}
