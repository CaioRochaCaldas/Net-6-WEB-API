namespace APICatalogo.Pagination
{
    public class CategoriasParameters : QueryStringParameters
    {
        //agora essa classe migrou para a classe abistrata QueryStringParameters
        //Isso ocorreu, pois Tanto CategoriasRepository e ProdutosRepository e suas devisas
        //interfaces vão usar pagina~ções 
    }
}
