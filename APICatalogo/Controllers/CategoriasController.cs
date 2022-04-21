using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }
        //Metodo especifico de categorias, pega todas as categorias + produtos(Include)
        //vai ter uma toda expecifica
        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            return _context.Categorias.Include(p=>p.Produtos).ToList();
        }

        //Todas as Categorias
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            return _context.Categorias.ToList();
        }
        //Uma Categoria por id qualquer ou uma que acabou de ser criada
        [HttpGet("{id}",Name ="ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(p=>p.CategoriaId == id);

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

                _context.Categorias.Add(categoria);
                _context.SaveChanges();

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

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
            
            if (categoria == null)
            {
                return NotFound("Categoria não encontrada");
            }
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return Ok(categoria);
        
        }

    }
}
