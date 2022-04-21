using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        //Coleção de lista de produto abaixo.
        //ActionResult faz aceitar a coleção de produtos caso ocorra um erro.
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _context.Produtos.ToList();
            if(produtos is null)
            {
                return NotFound();
            }
            return produtos;

        }

        //produto por id
        [HttpGet("{id}", Name="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if(produto == null)
            {
                return NotFound("Produto não encontrado");
            }

            return produto;
        }

        //Add novo produto
        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            _context.Produtos.Add(produto);//Add produto na memoria
            _context.SaveChanges(); // salva produtos e persiste eles

            //o produto que foi criado tem um id imcrementado e para achar
            //esse produto recem criado devemos fazer a instancia para o
            //codigo de produto criado 201 criação bem sucessida e podemos
            //achar ele por a rota ObterProduto que é a rota de apenas
            //produto recem criado

            return new CreatedAtRouteResult("ObterProduto",new {id = produto.ProdutoId},produto);
            
        }

        //Atualiza o produto tem como http os /produtos/{id}
        //OBS - o http put faz uma atualização completa em produto
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            //validação se o id passado é de algum produto
            if(id != produto.ProdutoId)
            {
                return BadRequest("Informe um produto que exista");
            }

            _context.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            //retorna o objeto produto que foi alterado status code 200 + o produto
            return Ok(produto);
        }

        //Deleta o produto com um id expecifico
        
        [HttpDelete("{id:int}")]
        
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p=>p.ProdutoId == id);//procura direto no banco
            //var produto = _context.Produtos.Find(id);   //procura na memoria e depois para o banco

            if (produto is null)
            {
                return NotFound("Produto não encontrado");
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            //retorna um produto excluido bem sucedidamente status code 200 + o proprio produto
            return Ok(produto);



        }


        


    }
}
