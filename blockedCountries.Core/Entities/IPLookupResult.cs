using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockedCountries.Core.Entities
{
    public class IPLookupResult
    {
        public string ISP { get; set; } = string.Empty;
        public string Country_Name { get; set; } = string.Empty;
        public string Country_Code2 { get; set; } = string.Empty;
        public string Continent_Code { get; set; } = string.Empty;
    }
}
        
  //"ip": "41.33.218.97",
  //"location": {
  //  "continent_code": "AF",
  //  "continent_name": "Africa",
  //  "country_code2": "EG",
  //  "country_code3": "EGY",
  //  "country_name": "Egypt",
  //  "country_name_official": "Arab Republic of Egypt",
  //  "country_capital": "Cairo",
  //  "state_prov": "Muhafazat al Qahirah",
  //  "state_code": "",
  //  "district": "Abdeen",
  //  "city": "Cairo",
  //  "locality": "Cairo",
  //  "accuracy_radius": "",
  //  "zipcode": "4272077",
  //  "latitude": "30.04442",
  //  "longitude": "31.23571",
  //  "is_eu": false,
  //  "country_flag": "https://ipgeolocation.io/static/flags/eg_64.png",
  //  "geoname_id": "10227598",
  //  "country_emoji": "🇪🇬"
  //},
  //"country_metadata": {
  //  "calling_code": "+20",
  //  "tld": ".eg",
  //  "languages": [
  //    "ar-EG",
  //    "en",
  //    "fr"
  //  ]
  //  },
  //"currency": {
  //  "code": "EGP",
  //  "name": "Egyptian Pound",
  //  "symbol": "E£"
