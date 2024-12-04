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
        private readonly IRepository<Produto> _repositoryProduto;

        public ProdutosController(IProdutosRepository repository, IRepository<Produto> repositoryProduto)
        {
            _repository = repository;
            _repositoryProduto = repositoryProduto;
        }

        [HttpGet("produtos/{id}")]
        public ActionResult<IEnumerable<Produto>> GetProdutosPorCategoria(int id)
        {
            var produtos = _repository.ObterProdutosPorCategoria(id);
            
            if (produtos is null)
                return NotFound();
            
            return Ok(produtos);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetProdutos()
        {
            var produtos = _repositoryProduto.GetAll();
            
            if (produtos is null)
                return NotFound("Produtos não encontrados");
                
            return Ok(produtos);
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public ActionResult<Produto> GetProdutoById(int id)
        {
            var produto = _repositoryProduto.GetById(x => x.ProdutoId == id);

            if (produto is null)
                return NotFound("Produto não encontrado");
            
            return Ok(produto);
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
                return BadRequest();
            
            var novoProduto = _repositoryProduto.Create(produto);

            return new CreatedAtRouteResult("ObterProduto", 
                new { id = novoProduto.ProdutoId }, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
                return BadRequest();
            
            var updateProduto = _repositoryProduto.Update(produto);
            
            if (updateProduto is not null)
                return Ok(produto);
            
            return StatusCode(500, "Falha ao atualizar o produto: " + produto.Nome + " do código: " + produto.ProdutoId);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _repositoryProduto.GetById(x => x.ProdutoId == id);
            
            if (produto is null)
                return NotFound("Esse produto não existe");
            
            var deleteProduto = _repositoryProduto.Delete(produto);
            
            if (deleteProduto is not null)
                return Ok("Produto deletado com sucesso");
            
            return StatusCode(500, "Falha ao deletar o produto com código: " + id);
        }
    }
}
    