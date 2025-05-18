using blockedCountries.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockedCountries.Core.Specification
{
    public class CountriesSpecSortAndPagination:BaseSpecification<BlockedCountries>
    {
        public CountriesSpecSortAndPagination(SpecPrams countriesPrams):base(
            p=>(string.IsNullOrEmpty(countriesPrams.Search))||
            p.CountryCode.Trim().ToLower().Contains(countriesPrams.Search.ToLower())||
            p.CountryName.Trim().ToLower().Contains(countriesPrams.Search.ToLower())
            )
        {
            AddPagination((countriesPrams.PageIndex - 1) * countriesPrams.PageSize, countriesPrams.PageSize);
            if(!string.IsNullOrEmpty(countriesPrams.Sort))
            {
                switch (countriesPrams.Sort)
                {
                    case "NameAsc":
                        AddOrderBy(p =>p.CountryName);
                        break;
                    case "NameDes":
                        AddOrderByDescending(p => p.CountryName);
                        break;
                    case "CodeAsc":
                        AddOrderByDescending(p => p.CountryCode);
                        break;
                    case "CodeDes":
                        AddOrderByDescending(p => p.CountryCode);
                        break;

                    default:
                        AddOrderBy(p => p.CountryName);
                        break;
                }
            }
        }
    }
}
