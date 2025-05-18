using blockedCountries.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockedCountries.Core.Specification
{
    public class CountriesFilterSpecification:BaseSpecification<BlockedCountries>
    {
        public CountriesFilterSpecification(SpecPrams countriesPrams) :base(

            p => (string.IsNullOrEmpty(countriesPrams.Search)) ||
            p.CountryCode.Trim().ToLower().Contains(countriesPrams.Search.ToLower()) ||
            p.CountryName.Trim().ToLower().Contains(countriesPrams.Search.ToLower())
            )
        {

        }
    }
}
