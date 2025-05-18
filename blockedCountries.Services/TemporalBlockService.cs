using blockedCountries.Core.Entities;
using blockedCountries.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace blockedCountries.Services
{
    public class TemporalBlockService : ITemporalBlockService
    {
        private readonly List<TemporalBlock> _temporalBlocks = new();
        public Task<bool> AddTemporalBlockAsync(string countryCode, int durationMinutes)
        {
            if (_temporalBlocks.Any(b => b.CountryCode.Equals(countryCode, StringComparison.OrdinalIgnoreCase)))
                return Task.FromResult(false);
            var block = new TemporalBlock
            {
                CountryCode = countryCode.ToUpper(),
                Expiration = DateTime.UtcNow.AddMinutes(durationMinutes)
            };

            _temporalBlocks.Add(block);
            return Task.FromResult(true);
        }

        public IReadOnlyList<TemporalBlock> GetAll()
       => _temporalBlocks;

        public Task<bool> IsTemporarilyBlockedAsync(string countryCode)
        {
            var now = DateTime.UtcNow;
            return Task.FromResult(_temporalBlocks.Any(b =>
                b.CountryCode.Equals(countryCode, StringComparison.OrdinalIgnoreCase) && b.Expiration > now));
        }

        public Task RemoveExpiredBlocksAsync()
        {
            var now = DateTime.UtcNow;
            _temporalBlocks.RemoveAll(b => b.Expiration <= now);
            return Task.CompletedTask;
        }
        public async Task<bool> IsValidCountryCode(string countryCode)
        {
            if (string.IsNullOrEmpty(countryCode) || !Regex.IsMatch(countryCode, "^[A-Z]{2}$"))
            {
                return false;
            }
            // Use CultureInfo to check if it's a valid ISO 3166-1 alpha-2 code.
            CultureInfo[] cultures =  CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            return await Task.FromResult( cultures.Any(culture => culture.Name.ToUpperInvariant() == countryCode.ToUpperInvariant()));
        }
    }
}
