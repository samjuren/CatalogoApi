using CatalogoApi.Context;
using CatalogoApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Controllers
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            if (_context.Produtos != null)
            {
                var produtos = await _context.Produtos.AsNoTracking().ToListAsync();

                if (produtos is null)
                    return NotFound("Produtos não encontrados");
                
                return produtos;
            }
            
            return NotFound("Problema ao acessar o banco de dados");
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public ActionResult<Produto> GetProdutoById(int id)
        {
            var produto = _context.Produtos
                .AsNoTracking()
                .FirstOrDefault(x => x.ProdutoId == id);

            if (produto is null)
                return NotFound("Produto não encontrado");
            
            return produto;
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
                return BadRequest();
            
            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", 
                new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
                return BadRequest();
            
            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();
            
            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);
            
            if (produto is null)
                return NotFound("Produto não encontrado");
            
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            
            return Ok(produto);
        }
    }
}
    