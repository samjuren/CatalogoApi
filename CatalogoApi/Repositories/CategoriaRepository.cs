using CatalogoApi.Context;
using CatalogoApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Repositories;

public class CategoriaRepository : Repository<Categoria>,ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
       
    }
}