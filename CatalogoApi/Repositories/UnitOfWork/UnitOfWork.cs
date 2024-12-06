using CatalogoApi.Context;
using CatalogoApi.Repositories.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace CatalogoApi.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private IProdutosRepository? _produtosRepository;
    private ICategoriaRepository? _categoriaRepository;
    public AppDbContext _context { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IProdutosRepository produtosRepository
    {
        get
        {
            return _produtosRepository = _produtosRepository ?? new ProsutosRepository(_context);
        }
        
    }

    public ICategoriaRepository categoriaRepository
    {
        get
        {
            return _categoriaRepository = _categoriaRepository ?? new CategoriaRepository(_context);
        }
    }

    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}