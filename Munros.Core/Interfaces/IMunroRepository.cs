using Munros.Core.Entities;
using System.Threading.Tasks;

namespace Munros.Core.Interfaces
{
    public interface IMunroRepository
    {
        Task<Munro[]> GetMunrosAsync(QueryParameters queryParameters);
        Task<Munro> GetMunroAsync(int id);
        Task<Munro> AddMunroAsync(Munro munro);
        Task<int> UpdateMunroAsync(Munro existingMunro, Munro munro);
        Task<int> DeleteMunroAsync(Munro existingMunro);
    }
}
