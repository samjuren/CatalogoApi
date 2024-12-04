using CatalogoApi.Context;
using CatalogoApi.Model;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Repositories;

public class ProsutosRepository : Repository<Produto>, IProdutosRepository
{
    public ProsutosRepository(AppDbContext context) : base(context)
    {
        
    }
    public IEnumerable<Produto> ObterProdutosPorCategoria(int categoriaId)
    {
        return GetAll().Where(c => c.CategoriaId == categoriaId);
    }
}