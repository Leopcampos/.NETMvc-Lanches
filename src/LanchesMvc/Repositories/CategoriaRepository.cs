using LanchesMvc.Data;
using LanchesMvc.Models;
using LanchesMvc.Repositories.Interfaces;

namespace LanchesMvc.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}