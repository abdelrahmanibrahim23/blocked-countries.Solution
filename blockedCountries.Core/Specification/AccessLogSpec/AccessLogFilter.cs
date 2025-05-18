using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockedCountries.Core.Specification.AccessLogSpec
{
    public class AccessLogFilter: BaseSpecification<Entities.AccessLog> 
    {
        public AccessLogFilter(SpecPrams spec):base(
            p => p.IsBlock
            )
        {
            
        }
    }
}
