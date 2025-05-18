using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockedCountries.Core.Entities
{
    public class BlockedCountries
    {
        public string CountryCode { get; set; }= string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public bool IsBlock { get; set; }
    }
}
