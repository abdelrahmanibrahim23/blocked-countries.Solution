using blockedCountries.Core.Entities;
using blockedCountries.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockedCountries.Core.Repositories
{
    public interface IAccessLog
    {
        Task Log(AccessLog log);
        Task<IReadOnlyList<AccessLog>> GetBlockLog(ISpecification<AccessLog> spec);
        Task<int> GetCountAsync(ISpecification<AccessLog> spec);
    }
}
