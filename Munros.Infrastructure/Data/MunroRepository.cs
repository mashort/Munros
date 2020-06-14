using Microsoft.EntityFrameworkCore;
using Munros.Core.Entities;
using Munros.Core.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Munros.Infrastructure.Data
{
    public class MunroRepository : IMunroRepository
    {
        private readonly AppDbContext _context;

        public MunroRepository(AppDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<Munro[]> GetMunrosAsync()
        {
            IQueryable<Munro> munros = _context.Munros;

            return await munros.ToArrayAsync();
        }
    }
}
