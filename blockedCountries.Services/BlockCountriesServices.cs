using blockedCountries.Core.Entities;
using blockedCountries.Core.Repositories;
using blockedCountries.Core.Specification;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockedCountries.Services
{
    public class BlockCountriesServices : IBlockCountries
    {
        private readonly List<BlockedCountries> _blockedCountries =  new ();
        public   async Task<IReadOnlyList<BlockedCountries>> GetAllAsync(ISpecification<BlockedCountries> spec)
        {

            var result = ApplySpecification(spec).ToList(); 
            return await Task.FromResult<IReadOnlyList<BlockedCountries>>(result);

        }

        public Task AddAsync(BlockedCountries block)
        {
            if (!_blockedCountries.Any(C=>C.CountryCode.Equals(block.CountryCode,StringComparison.OrdinalIgnoreCase)))
            {
                _blockedCountries.Add(block);
            }
            return Task.CompletedTask;
        }


        public Task<bool> RemoveAsync(string CountryCode)
        {
            var country =_blockedCountries.FirstOrDefault(c=>c.CountryCode.Equals(CountryCode,StringComparison.OrdinalIgnoreCase));
            if (country != null)
            {
                _blockedCountries.Remove(country);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
        public IQueryable<BlockedCountries> ApplySpecification(ISpecification<BlockedCountries> spec)
        {
            return SpecificationEvaluation<BlockedCountries>.GetQuery(_blockedCountries.AsQueryable(),spec);
        }

        public async Task<int> GetCountAsync(ISpecification<BlockedCountries> spec)
        {

            int result = ApplySpecification(spec).Count();
            return await Task.FromResult<int>(result);
        }

        public Task<bool> GetIsBlock(string CountryCode)
        {
            var isBlocked = _blockedCountries.Any(c =>
            string.Equals(c.CountryCode?.Trim(), CountryCode?.Trim(), StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(isBlocked);
        }
    }
}
