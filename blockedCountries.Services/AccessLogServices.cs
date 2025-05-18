using blockedCountries.Core.Entities;
using blockedCountries.Core.Repositories;
using blockedCountries.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockedCountries.Services
{
    public class AccessLogServices : IAccessLog

    {


        private readonly List<AccessLog> _accessLog = new();
        public async Task<IReadOnlyList<AccessLog>> GetBlockLog(ISpecification<AccessLog> spec)
        {
            var result = ApplySpecification(spec).ToList();
            return await Task.FromResult<IReadOnlyList<AccessLog>>(result); ;
        }

        public  Task Log(AccessLog log)
        {
             _accessLog.Add(log);
            return Task.CompletedTask;
        }
        public IQueryable<AccessLog> ApplySpecification(ISpecification<AccessLog> spec)
        {
            return SpecificationEvaluation<AccessLog>.GetQuery(_accessLog.AsQueryable(), spec);
        }
        public async Task<int> GetCountAsync(ISpecification<AccessLog> spec)
        {

            int result = ApplySpecification(spec).Count();
            return await Task.FromResult<int>(result);
        }
    }
}
