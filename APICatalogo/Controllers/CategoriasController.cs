using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitofWork _uow;

        public CategoriasController(IUnitofWork context)
        {
            _uow = context;
        }
        //Metodo especifico de categorias, pega todas as categorias + produtos(Include)
        //vai ter uma toda expecifica

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            // return _uow.CategoriaRepository.Include(p=>p.Produtos).ToList();
            return _uow.CategoriaRepository.GetCategoriasProdutos().ToList();
        }
        //
        //Todas as Categorias
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            return _uow.CategoriaRepository.Get().ToList();
        }

        //Uma Categoria por id qualquer ou uma que acabou de ser criada
        [HttpGet("{id}",Name ="ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _uow.CategoriaRepository.GetById(p=>p.CategoriaId == id);

            if(categoria == null)
            {
                return NotFound("Categoria não encontrada");
            }
            return Ok(categoria);
        }

        //Criar novo produto e o achar com seu id novo auto gerado
        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if(categoria == null) return BadRequest();

                _uow.CategoriaRepository.Add(categoria);
                _uow.Commit();

                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
            
        }
        //atualiza uma categoria
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            _uow.CategoriaRepository.Update(categoria);
            _uow.Commit();
            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> Delete(int id)
        {
            var categoria = _uow.CategoriaRepository.GetById(p => p.CategoriaId == id);
            
            if (categoria == null)
            {
                return NotFound("Categoria não encontrada");
            }
            _uow.CategoriaRepository.Delete(categoria);
            _uow.Commit();
            return Ok(categoria);
        
        }

    }
}
