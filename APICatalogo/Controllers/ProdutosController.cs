using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitofWork _uow;
        private readonly IMapper _mapper;

        public ProdutosController(IUnitofWork context, IMapper mapper)
        {
            _uow = context;
            _mapper = mapper;
        }
        //produtos por preço
        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPrecos()
        {   //O acesso é feito por  ProdutoRepository apos o filtro dentro deProdutoDTO retorna um <IEnumerable<ProdutoDTO>>
            var produtos = _uow.ProdutoRepository.GetProdutosPorPreco().ToList();
             var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);
             return produtosDto;
        }

      
        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDTO>> Get()
        {
            var produtos = _uow.ProdutoRepository.Get().ToList();
            var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);
            return produtosDto;

        }

      
        [HttpGet("{id}", Name="ObterProduto")]
        public ActionResult<ProdutoDTO> Get(int id)
        {
            var produto = _uow.ProdutoRepository.GetById(p=>p.ProdutoId ==id);
            if(produto == null)
            {
                return NotFound("Produto não encontrado");
            }
            var produtoDto = _mapper.Map<ProdutoDTO>(produto);

            return produtoDto;
        }

        //Add novo produto
        [HttpPost]
        public ActionResult Post(ProdutoDTO produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);
            _uow.ProdutoRepository.Add(produto);
            _uow.Commit(); 

            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

            return new CreatedAtRouteResult("ObterProduto",new {id = produto.ProdutoId},produtoDTO);
            
        }

        //Atualiza o produto tem como http os /produtos/{id}
        //OBS - o http put faz uma atualização completa em produto
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, ProdutoDTO produtoDto)
        {
            //validação se o id passado é de algum produto
            if(id != produtoDto.ProdutoId)
            {
                return BadRequest("Informe um produto que exista");
            }

            var produto = _mapper.Map<Produto>(produtoDto);

            _uow.ProdutoRepository.Update(produto);
            _uow.Commit();

            //retorna o objeto produto que foi alterado status code 200 + o produto
            return Ok(produto);
        }

        //Deleta o produto com um id expecifico
        
        [HttpDelete("{id:int}")]
        
        public ActionResult<ProdutoDTO> Delete(int id)
        {
            var produto = _uow.ProdutoRepository.GetById(p=>p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não encontrado");
            }

            _uow.ProdutoRepository.Delete(produto);
            _uow.Commit();

            var produtoDto = _mapper.Map<ProdutoDTO>(produto);
            return produtoDto;

        }

    }
}
