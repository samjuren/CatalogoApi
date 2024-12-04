using CatalogoApi.Model;

namespace CatalogoApi.Repositories;

public interface IProdutosRepository
{
    IEnumerable<Produto> GetProdutos();
    Produto GetProduto(int id);
    Produto Create(Produto produto);
    bool Update(Produto produto);
    Produto Delete(int id);
}