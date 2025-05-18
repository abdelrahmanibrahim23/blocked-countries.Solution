using blockedCountries.Core.Entities;
using blockedCountries.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockedCountries.Core.Repositories
{
    public interface IBlockCountries
    {
        Task<IReadOnlyList<BlockedCountries>> GetAllAsync(ISpecification<BlockedCountries> spec);
        Task AddAsync(BlockedCountries block);
        Task<bool> RemoveAsync(string CountryCode);
        Task<int> GetCountAsync(ISpecification<BlockedCountries> spec);
        Task<bool> GetIsBlock(string CountryCode);

    }
}
