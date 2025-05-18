using blockedCountries.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockedCountries.Core.Repositories
{
    public interface IIPAddress
    {
        Task<IPLookupResult> GetLookupAddress(string url);
    }
}
