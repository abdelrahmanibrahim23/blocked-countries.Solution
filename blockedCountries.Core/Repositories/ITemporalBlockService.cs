using blockedCountries.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockedCountries.Core.Repositories
{
    public interface ITemporalBlockService
    {
        Task<bool> AddTemporalBlockAsync(string countryCode, int durationMinutes);
        Task<bool> IsTemporarilyBlockedAsync(string countryCode);
        Task RemoveExpiredBlocksAsync();
        IReadOnlyList<TemporalBlock> GetAll();
        Task<bool> IsValidCountryCode(string countryCode);
    }
}
