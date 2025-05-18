using AutoMapper;
using blocked_countries.DTO;
using blocked_countries.Errors;
using blocked_countries.Helper;
using blockedCountries.Core.Entities;
using blockedCountries.Core.Repositories;
using blockedCountries.Core.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace blocked_countries.Controllers
{

    public class CountryController : BaseApiController
    {
        private readonly IBlockCountries _blockCountries;
        private readonly IMapper _mapper;
        private readonly ITemporalBlockService _temporalBlockService;

        public CountryController(IBlockCountries blockCountries,IMapper mapper,ITemporalBlockService temporalBlockService) {
            _blockCountries = blockCountries;
            _mapper = mapper;
            _temporalBlockService = temporalBlockService;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<CountriesDTO>>> GetAll([FromQuery]SpecPrams spec) {
            var specification = new CountriesSpecSortAndPagination(spec);
            var countries=await _blockCountries.GetAllAsync(specification);
            var Data=_mapper.Map<IReadOnlyList<BlockedCountries>, IReadOnlyList< CountriesDTO >>(countries);
            var countSpec = new CountriesFilterSpecification(spec);
            var count= await _blockCountries.GetCountAsync(countSpec);
            if (count==0) 
                return NotFound(new ApiResponse(404));

            return Ok(new Pagination<CountriesDTO>(spec.PageIndex,spec.PageSize,count,Data));
        }
        [HttpPost]
        public  async Task<ActionResult<BlockedCountries>> CreateCountries([FromBody] CountriesDTO data)
        {
            var Data = _mapper.Map<CountriesDTO, BlockedCountries >(data);
             await _blockCountries.AddAsync(Data);
            return Ok(Data);
        }
        [HttpDelete("{CountryCode}")]
        public async Task<ActionResult<BlockedCountries>> RemoveCountry(string CountryCode)
        {
           var result = await _blockCountries.RemoveAsync(CountryCode);
            if (!result)
                return BadRequest(new ApiResponse(500));
            
            return Ok(result);
        }
        [HttpPost("temporal-block")]
        public async Task<ActionResult<TemporalBlockRequestDTO>> AddTempBlock([FromBody]TemporalBlockRequestDTO temp)
        {
            if (temp.DurationMinutes < 1 || temp.DurationMinutes >= 1440)
                return BadRequest(new ApiResponse(500, "Duration must be between 1 and 1440 minutes."));
            bool result=await _temporalBlockService.AddTemporalBlockAsync(temp.CountryCode, temp.DurationMinutes);
            if(!result)
                return BadRequest(new ApiResponse(500, "Invalid country code format.  Must be two uppercase letters and a valid ISO 3166-1 alpha-2 code."));
            return Ok(temp);

        }
        [HttpGet("temporal-block")]
        public async Task<ActionResult<TemporalBlockRequestDTO>> GetAllTemporal()
        {
            var data =  _temporalBlockService.GetAll();
            return Ok(data);
        }


    }

}
