using CatalogoApi.Context;
using CatalogoApi.Model;
using CatalogoApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutosRepository _repository;
        
        public ProdutosController(IProdutosRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetProdutos()
        {
            var produtos = _repository.GetProdutos().ToList();
            
            if (produtos is null)
                return NotFound("Produtos não encontrados");
                
            return Ok(produtos);
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public ActionResult<Produto> GetProdutoById(int id)
        {
            var produto = _repository.GetProduto(id);

            if (produto is null)
                return NotFound("Produto não encontrado");
            
            return Ok(produto);
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
                return BadRequest();
            
            var novoProduto = _repository.Create(produto);

            return new CreatedAtRouteResult("ObterProduto", 
                new { id = novoProduto.ProdutoId }, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
                return BadRequest();
            
            var updateProduto = _repository.Update(produto);
            
            if (updateProduto)
                return Ok(produto);
            
            return StatusCode(500, "Falha ao atualizar o produto: " + produto.Nome + " do código: " + produto.ProdutoId);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var deleteProduto = _repository.Delete(id);
            
            if (deleteProduto)
                return Ok("Produto deletado com sucesso");
            
            return StatusCode(500, "Falha ao deletar o produto com código: " + id);
        }
    }
}
    