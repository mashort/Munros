using Microsoft.EntityFrameworkCore;
using Munros.Core.Entities;
using Munros.Core.Interfaces;
using System.Linq;
using System.Reflection;
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

            munros = munros.Where(m => !string.IsNullOrEmpty(m.Category));

            if (!string.IsNullOrEmpty(queryParameters.Category) && queryParameters.Category.ToLower() != "either")
            {
                munros = munros.Where(m => m.Category.ToLower() == queryParameters.Category.ToLower());
            }

            if (queryParameters.MinHeight != null)
            {
                munros = munros.Where(m => m.Height >= queryParameters.MinHeight);
            }

            if (queryParameters.MaxHeight != null)
            {
                munros = munros.Where(m => m.Height <= queryParameters.MaxHeight);
            }

            if (!string.IsNullOrEmpty(queryParameters.SortBy))
            {
                // Check if the name of the property passed in through SortBy is a property of the Munro entity
                if (typeof(Munro).GetProperty(queryParameters.SortBy, 
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null)
                {
                    munros = munros.OrderByCustom(queryParameters.SortBy, queryParameters.SortOrder);
                }
            }
            
            if (queryParameters.ResultsLimit > 0)
            {
                munros = munros.Take(queryParameters.ResultsLimit);
            }

            return await munros.ToArrayAsync();
        }
    }
}
