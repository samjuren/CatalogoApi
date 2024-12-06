using CatalogoApi.Context;
using CatalogoApi.Model;
using CatalogoApi.Repositories;
using CatalogoApi.Repositories.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public CategoriasController(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
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
        
        [HttpGet]
        //[ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Categoria>> GetCategorias()
        {
            try
            {
                var categorias = _unitOfWork.categoriaRepository.GetAll();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao carregar o categoria");
            }
        }
        
        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> GetCategoriaById(int id)
        {
            //_logger.LogInformation("######## GET api/categorias/if " + id + " ########/");
            //throw new Exception("Exceção ao retornar o categoria");

            var categoria = _unitOfWork.categoriaRepository.GetById(c => c.CategoriaId == id);
            
            if (categoria is null)
            {
                //_logger.LogWarning("Categoria não encontrada");
                return NotFound("Categoria não encontrado");
            }
            
            return Ok(categoria);
        }
        
        [HttpPost]
        public ActionResult Post(Categoria categorias)
        {
            if (categorias is null)
                return BadRequest();

            var categoriaCriada = _unitOfWork.categoriaRepository.Create(categorias);
            _unitOfWork.Commit();
            
            return new CreatedAtRouteResult("ObterProduto", 
                new { id = categoriaCriada.CategoriaId }, categoriaCriada);
        }
        
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categorias)
        {
            if (id != categorias.CategoriaId)
            {
                return BadRequest("Dados invalidos");
            }
            
            _unitOfWork.categoriaRepository.Update(categorias);
            _unitOfWork.Commit();
            
            return Ok(categorias);
        }
        
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _unitOfWork.categoriaRepository.GetById(c => c.CategoriaId == id);
            
            if (categoria is null)
                return NotFound("Categoria não encontrada");
            
            var categoriaExcluida = _unitOfWork.categoriaRepository.Delete(categoria);
            _unitOfWork.Commit();
            
            return Ok(categoriaExcluida);
        }
    }
}
