using System.ComponentModel.DataAnnotations;

namespace blocked_countries.DTO
{
    public class AccessDTO
    {
        
        [Required]
        public string Ip { get; set; } = string.Empty;
        [Required]
        public string Country_Code2 { get; set; } = string.Empty;
        [Required]
        public bool IsBlock { get; set; }
       
    }
}
