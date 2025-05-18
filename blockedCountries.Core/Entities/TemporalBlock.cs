using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockedCountries.Core.Entities
{
    public class TemporalBlock
    {
        public string CountryCode { get; set; }
        public DateTime Expiration { get; set; }
    }
}
