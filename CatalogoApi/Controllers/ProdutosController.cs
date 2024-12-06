using CatalogoApi.Context;
using CatalogoApi.Model;
using CatalogoApi.Repositories;
using CatalogoApi.Repositories.UnitOfWork.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProdutosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("produtos/{id}")]
        public ActionResult<IEnumerable<Produto>> GetProdutosPorCategoria(int id)
        {
            var produtos = _unitOfWork.produtosRepository.ObterProdutosPorCategoria(id);
            
            if (produtos is null)
                return NotFound();
            
            return Ok(produtos);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetProdutos()
        {
            var produtos = _unitOfWork.produtosRepository.GetAll();
            
            if (produtos is null)
                return NotFound("Produtos não encontrados");
                
            return Ok(produtos);
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public ActionResult<Produto> GetProdutoById(int id)
        {
            var produto = _unitOfWork.produtosRepository.GetById(x => x.ProdutoId == id);

            if (produto is null)
                return NotFound("Produto não encontrado");
            
            return Ok(produto);
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
                return BadRequest();
            
            var novoProduto = _unitOfWork.produtosRepository.Create(produto);
            _unitOfWork.Commit();

            return new CreatedAtRouteResult("ObterProduto", 
                new { id = novoProduto.ProdutoId }, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
                return BadRequest();
            
            var updateProduto = _unitOfWork.produtosRepository.Update(produto);
            _unitOfWork.Commit();
            
            if (updateProduto is not null)
                return Ok(produto);
            
            return StatusCode(500, "Falha ao atualizar o produto: " + produto.Nome + " do código: " + produto.ProdutoId);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _unitOfWork.produtosRepository.GetById(x => x.ProdutoId == id);
            
            if (produto is null)
                return NotFound("Esse produto não existe");
            
            var deleteProduto = _unitOfWork.produtosRepository.Delete(produto);
            _unitOfWork.Commit();
            
            if (deleteProduto is not null)
                return Ok("Produto deletado com sucesso");
            
            return StatusCode(500, "Falha ao deletar o produto com código: " + id);
        }
    }
}
    