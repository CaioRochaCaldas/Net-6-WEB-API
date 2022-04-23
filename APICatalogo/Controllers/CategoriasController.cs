using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repository;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public CategoriasController(IUnitofWork context,IMapper mapper)
        {
            _uow = context;
            _mapper = mapper;
        }
       

        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasProdutos() //agora é async
        {
            var categorias = await _uow.CategoriaRepository.GetCategoriasProdutos();
            var categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);
            return categoriasDto;

        }
        //
        //Todas as Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] CategoriasParameters categoriasParameters)
        {
            var categorias = await _uow.CategoriaRepository.GetCategoriasPaginas(categoriasParameters);

            var metadata = new { 
                categorias.TotalCount,
                categorias.PageSize,
                categorias.CurrentPage,
                categorias.TotalPages,
                categorias.HasNext,
                categorias.HasPrevious

            };

            //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);
            return categoriasDto;
        }

        //Uma Categoria por id qualquer ou uma que acabou de ser criada
        [HttpGet("{id}",Name ="ObterCategoria")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {
            var categoria = await _uow.CategoriaRepository.GetById(p=>p.CategoriaId == id);

            if(categoria == null)
            {
                return NotFound("Categoria não encontrada");
            }
            var categoriasDto = _mapper.Map<CategoriaDTO>(categoria);
            return categoriasDto;
        }

        //Criar novo produto e o achar com seu id novo auto gerado
        [HttpPost]
        public async Task<ActionResult> Post(CategoriaDTO categoriaDto)
        {
                var categoria = _mapper.Map<Categoria>(categoriaDto);
                _uow.CategoriaRepository.Add(categoria);
                await _uow.Commit();

                var produtoDTO = _mapper.Map<CategoriaDTO>(categoria); 

                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, produtoDTO);
            
        }
        //atualiza uma categoria
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, CategoriaDTO categoriaDto)
        {
            if (id != categoriaDto.CategoriaId)
            {
                return BadRequest();
            }
            var categoria = _mapper.Map<Categoria>(categoriaDto);

            _uow.CategoriaRepository.Update(categoria);
            _uow.Commit();
            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> Delete(int id)
        {
            var categoria = await _uow.CategoriaRepository.GetById(p => p.CategoriaId == id);
            
            if (categoria == null)
            {
                return NotFound("Categoria não encontrada");
            }

            _uow.CategoriaRepository.Delete(categoria);
            await _uow.Commit();

            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);    
            return categoriaDto;
        
        }

    }
}
