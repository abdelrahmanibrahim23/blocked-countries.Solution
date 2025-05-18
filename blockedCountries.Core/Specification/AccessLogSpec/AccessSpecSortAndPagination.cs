using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockedCountries.Core.Specification.AccessLogSpec
{
    public class AccessSpecSortAndPagination :BaseSpecification<Entities.AccessLog> 
    { 
        public AccessSpecSortAndPagination(SpecPrams AccessPrams):base(
            p=>p.IsBlock
            
            )
        {
            AddPagination((AccessPrams.PageIndex - 1) * AccessPrams.PageSize, AccessPrams.PageSize);
            if (!string.IsNullOrEmpty(AccessPrams.Sort))
            {
                switch (AccessPrams.Sort)
                {
                    case "TimeAsc":
                        AddOrderBy(p => p.Timestamp);
                        break;
                    case "TimeDes":
                        AddOrderByDescending(p => p.Timestamp);
                        break;
                    default:
                        AddOrderBy(p => p.Country_Code2);
                        break;
                }
            }
        }
    }
}
