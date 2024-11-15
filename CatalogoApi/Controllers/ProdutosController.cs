using CatalogoApi.Context;
using CatalogoApi.Model;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IEnumerable<Produto>> GetProdutos()
        {
            var produtos = _context.Produtos.ToList();

            if (produtos is null)
            {
                return NotFound("Produtos não encontrados");
            }
            
            return produtos;
        }

        [HttpGet("{id:int}")]
        public ActionResult<Produto> GetProdutoById(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não encontrado");
            }
            
            return produto;
        }
    }
}
