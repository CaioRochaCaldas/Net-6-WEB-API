namespace APICatalogo.Repository
{
    public interface IUnitofWork
    {

        IProdutoRepository ProdutoRepository { get; }
        ICategoriaRepository CategoriaRepository { get; }

        //metodo para salvar auterações
        void Commit();
    }
}
