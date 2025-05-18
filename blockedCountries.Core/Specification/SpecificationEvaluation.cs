using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockedCountries.Core.Specification
{
    public class SpecificationEvaluation<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery,ISpecification<T> spec)
        {
            var query = inputQuery;
            if (spec.Criteria!=null)
            {
                query = query.Where(spec.Criteria);
            }
            if (spec.PaginationExecution)
            {
                query=query.Skip(spec.Skip).Take(spec.Take);
            }
            if (spec.OrderBy!=null)
            {
                query= query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDescending!=null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            return query;
        }
    }
}
