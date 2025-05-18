using blockedCountries.Core.Entities;
using blockedCountries.Core.Repositories;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace blockedCountries.Services
{
    public class IPAddressServices : IIPAddress
    {
        private readonly HttpClient _httpClient;

        public IPAddressServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IPLookupResult?> GetLookupAddress(string url)
        {
            var response=await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;
            var json = await response.Content.ReadAsStringAsync();
            if (json.StartsWith("<"))
                return null;
            return JsonSerializer.Deserialize<IPLookupResult>(json,new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive=true
            });
            
        }
    }
}
