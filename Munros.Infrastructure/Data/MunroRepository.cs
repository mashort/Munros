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

            if (!string.IsNullOrEmpty(queryParameters.PrimarySortBy))
            {
                // Check if the name of the property passed in through PrimarySortBy is a property of the Munro entity
                if (typeof(Munro).GetProperty(queryParameters.PrimarySortBy,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null)
                {
                    munros = munros.OrderByCustom(queryParameters.PrimarySortBy, queryParameters.PrimarySortOrder);

                    if (!string.IsNullOrEmpty(queryParameters.SecondarySortBy))
                    {
                        // Check if the name of the property passed in through SecondarySortBy is a property of the Munro entity
                        if (typeof(Munro).GetProperty(queryParameters.SecondarySortBy,
                            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null)
                        {
                            munros = munros.ThenByCustom(queryParameters.SecondarySortBy, queryParameters.SecondarySortOrder);
                        }
                    }
                }
            }

            // Hardcode the SortBy,SortOrder expression to get a static version working initially
            //munros = munros.OrderByDescending(t => t.Height)
            //    .ThenBy(s => s.Name);

            if (queryParameters.ResultsLimit > 0)
            {
                munros = munros.Take(queryParameters.ResultsLimit);
            }

            return await munros.ToArrayAsync();
        }

        public async Task<Munro> GetMunroAsync(int id)
        {
            return await _context.Munros.FindAsync(id);
        }
        
        public async Task<Munro> AddMunroAsync(Munro munro)
        {
            await _context.Munros.AddAsync(munro);
            await _context.SaveChangesAsync();

            return munro;
        }

        public async Task<int> UpdateMunroAsync(Munro existingMunro, Munro munro)
        {
            //_context.Entry(munro).State = EntityState.Modified;
            _context.Entry(existingMunro).CurrentValues.SetValues(munro);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteMunroAsync(Munro existingMunro)
        {
            _context.Munros.Remove(existingMunro);
            return await _context.SaveChangesAsync();
        }
    }
}
