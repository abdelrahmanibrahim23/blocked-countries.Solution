namespace blocked_countries.DTO
{
    public class TemporalBlockRequestDTO
    {
        public string CountryCode     { get; set; } = default!;
        public int    DurationMinutes { get; set; }
    }
}
