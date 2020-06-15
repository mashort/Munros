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

        public async Task<Munro[]> GetMunrosAsync(QueryParameters queryParameters)
        {
            IQueryable<Munro> munros = _context.Munros;

            if (!string.IsNullOrEmpty(queryParameters.Category) && queryParameters.Category.ToLower() != "either")
            {
                munros = munros.Where(m => m.Category.ToLower() == queryParameters.Category.ToLower());
            }

            munros = munros.Where(m => !string.IsNullOrEmpty(m.Category));

            return await munros.ToArrayAsync();
        }
    }
}
