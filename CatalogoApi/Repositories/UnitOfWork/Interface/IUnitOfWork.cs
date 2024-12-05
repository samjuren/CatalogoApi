namespace CatalogoApi.Repositories.UnitOfWork.Interface;

public interface IUnitOfWork
{
    IProdutosRepository produtosRepository { get;}
    ICategoriaRepository categoriaRepository { get;}
    void Commit();
}