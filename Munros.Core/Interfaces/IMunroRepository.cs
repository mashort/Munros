using Munros.Core.Entities;
using System.Threading.Tasks;

namespace Munros.Core.Interfaces
{
    public interface IMunroRepository
    {
        Task<Munro[]> GetMunrosAsync(QueryParameters queryParameters);
    }
}
