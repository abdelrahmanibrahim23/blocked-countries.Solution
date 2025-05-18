using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockedCountries.Core.Entities
{
    public class AccessLog
    {
        public DateTime Timestamp { get; set; }
        public string Ip { get; set; } = string.Empty;
        public string Country_Code2 { get; set; } = string.Empty;
        public bool IsBlock { get; set; }
        public string UserAgent { get; set; } = string.Empty;

    }
}
