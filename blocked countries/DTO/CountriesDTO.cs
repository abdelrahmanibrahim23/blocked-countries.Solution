using System.ComponentModel.DataAnnotations;

namespace blocked_countries.DTO
{
    public class CountriesDTO
    {
        [Required]
        public string CountryCode { get; set; }=string.Empty;
        [Required]
        public string CountryName { get; set; } = string.Empty;
        [Required]
        public bool IsBlock { get; set; }

    }
}
