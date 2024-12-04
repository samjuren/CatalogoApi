using CatalogoApi.Model;

namespace CatalogoApi.Repositories;

public interface IProdutosRepository :IRepository<Produto>
{
    IEnumerable<Produto> ObterProdutosPorCategoria(int categoriaId);
}