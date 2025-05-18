using AutoMapper;
using blocked_countries.DTO;
using blockedCountries.Core.Entities;

namespace blocked_countries.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<BlockedCountries,CountriesDTO>().ReverseMap();
            CreateMap<AccessLog,AccessDTO>()
           .ForMember(dest => dest.Ip, opt => opt.Ignore()) // We'll set IP manually
           .ForMember(dest => dest.IsBlock, opt => opt.Ignore()).ReverseMap(); // Set manually too
        }

    }
}
