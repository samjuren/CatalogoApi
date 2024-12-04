using CatalogoApi.Context;
using CatalogoApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Categoria> GetCategorias()
    {
        return _context.Categorias.ToList();
    }

    public Categoria GetCategoria(int id)
    {
        return _context.Categorias.FirstOrDefault(x => x.CategoriaId == id);
    }

    public Categoria Create(Categoria categoria)
    {
        if (categoria is null)
            throw new ArgumentNullException(nameof(categoria));
        
        _context.Categorias.Add(categoria);
        _context.SaveChanges();
        
        return categoria;
    }

    public Categoria Update(Categoria categoria)
    {
        if (categoria is null)
            throw new ArgumentNullException(nameof(categoria));
        
        _context.Entry(categoria).State = EntityState.Modified;
        //_context.Categorias.Update(categoria);
        _context.SaveChanges();
        
        return categoria;
    }

    public Categoria Delete(int id)
    {
        var categoriaExcluir = _context.Categorias.Find(id);
        if (categoriaExcluir is null)
            throw new ArgumentNullException(nameof(categoriaExcluir));
        
        _context.Categorias.Remove(categoriaExcluir);
        _context.SaveChanges();
        
        return categoriaExcluir;
    }
}