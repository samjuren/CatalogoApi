using CatalogoApi.Context;
using CatalogoApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        
        public CategoriasController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet("LerConfiguracao")]
        public string GetValores()
        {
            var valor1 = _config["chave1"];
            var valor2 = _config["chave2"];
            
            var secao1 = _config["secao:chave2"];
            
            return $"valor1: {valor1}, valor2: {valor2},  secao1: {secao1}";
        }

        [HttpGet("Produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasEProdutos()
        {
            return _context.Categorias
                .AsNoTracking()
                .Include(p => p.Produtos)
                .ToList();
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> GetCategorias()
        {
            try
            {
                var categorias = _context.Categorias.AsNoTracking().ToList();
                
                if (categorias is null)
                    return NotFound("Produtos não encontrados");
            
                return categorias;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao carregar o categoria");
            }
        }
        
        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> GetCategoriaById(int id)
        {
            //throw new Exception("Exceção ao retornar o categoria");
            
            var categorias = _context.Categorias
                .AsNoTracking()
                .FirstOrDefault(x => x.CategoriaId == id);

            if (categorias is null)
            {
                return NotFound("Produto não encontrado");
            }
            
            return categorias;
        }
        
        [HttpPost]
        public ActionResult Post(Categoria categorias)
        {
            if (categorias is null)
                return BadRequest();
            
            _context.Categorias.Add(categorias);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", 
                new { id = categorias.CategoriaId }, categorias);
        }
        
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categorias)
        {
            if (id != categorias.CategoriaId)
                return BadRequest();
            
            _context.Entry(categorias).State = EntityState.Modified;
            _context.SaveChanges();
            
            return Ok(categorias);
        }
        
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(x => x.CategoriaId == id);
            
            if (categoria is null)
                return NotFound("Produto não encontrado");
            
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            
            return Ok(categoria);
        }
    }
}
