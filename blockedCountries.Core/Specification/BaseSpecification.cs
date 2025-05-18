using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace blockedCountries.Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool PaginationExecution { get; set; }
        public Expression<Func<T, object>> OrderBy { get ; set ; }
        public Expression<Func<T, object>> OrderByDescending { get ; set ; }

        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T,bool>> criteria)
        {
            Criteria = criteria;
        }
        public void AddPagination(int skip, int take) {
            Skip = skip;
            Take = take;
            PaginationExecution = true;
        }
        public void AddOrderBy(Expression<Func<T,object>> orderBy)
        {
            orderBy=OrderBy;
        }
        public void AddOrderByDescending(Expression<Func<T,object>> orderByDescending)
        {
            orderByDescending=OrderByDescending;
        }

    }
}
