using CatalogoApi.Context;
using CatalogoApi.Model;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Repositories;

public class ProsutosRepository : IProdutosRepository
{
    private readonly AppDbContext _context;

    public ProsutosRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Produto> GetProdutos()
    {
        return _context.Produtos;
    }

    public Produto GetProduto(int id)
    {
        var produtoById = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);

        if (produtoById is null)
            throw new InvalidOperationException("Esse produto não existe");
        
        return produtoById;
    }

    public Produto Create(Produto produto)
    {
        if (produto is null)
            throw new ArgumentNullException(nameof(produto));
        
        _context.Produtos.Add(produto);
        _context.SaveChanges();
        
        return produto;
    }

    public bool Update(Produto produto)
    {
        if (produto is null)
            throw new ArgumentNullException(nameof(produto));

        if (!_context.Produtos.Any(x => x.ProdutoId == produto.ProdutoId))
            return false;
        
        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();
            
        return true;
    }

    public bool Delete(int id)
    {
        var produtosExcluir = _context.Produtos.Find(id);
        
        if (produtosExcluir is null)
            throw new ArgumentNullException(nameof(produtosExcluir));
        
        _context.Produtos.Remove(produtosExcluir);
        _context.SaveChanges();
        
        return true;
    }
}