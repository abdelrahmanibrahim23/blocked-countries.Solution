using AutoMapper;
using blocked_countries.DTO;
using blocked_countries.Errors;
using blockedCountries.Core.Entities;
using blockedCountries.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography.Xml;

namespace blocked_countries.Controllers
{

    public class IPController : BaseApiController
    {
        private readonly IConfiguration _config;
        private readonly IIPAddress _ipAddress;
        private readonly IBlockCountries _blockCountries;
        private readonly IAccessLog _accessLog;
        private readonly IMapper _mapper;
        private readonly ITemporalBlockService _temporalBlockService;

        public IPController(IConfiguration config,
            IIPAddress ipAddress,
            IBlockCountries blockCountries,
            IAccessLog accessLog,
            IMapper mapper,
            ITemporalBlockService temporalBlockService
            )
        {
            _config = config;
            _ipAddress = ipAddress;
            _blockCountries = blockCountries;
            _accessLog = accessLog;
            _mapper = mapper;
            _temporalBlockService = temporalBlockService;
        }
        [HttpGet("lookup")]
        public async Task<ActionResult<IPLookupResult>> GetIpInformation([FromQuery]string? ipAddress) { 
            ipAddress ??=HttpContext.Connection.RemoteIpAddress?.ToString();
            if (string.IsNullOrWhiteSpace(ipAddress) || !IPAddress.TryParse(ipAddress, out _))
                return BadRequest(new ApiResponse(400));
            var baseUrl = _config["IpApi:BaseUrl"];
            var ApiKey = _config["IpApi:ApiKey"];
            var url = $"{baseUrl}?apiKey={ApiKey}&ip={ipAddress}";
            var data = await _ipAddress.GetLookupAddress(url);
           return Ok(data);
        }
        [HttpGet("Check-Block")]
        public async Task<ActionResult<AccessDTO>> CheckBlockAndLog()
        {
           var ipAddress = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault()
         ?? HttpContext.Connection.RemoteIpAddress?.ToString()
         ?? "127.0.0.1";
            var baseUrl = _config["IpApi:BaseUrl"];
            var ApiKey = _config["IpApi:ApiKey"];
            var url = $"{baseUrl}?apiKey={ApiKey}&ip={ipAddress}";
            var data = await _ipAddress.GetLookupAddress(url);
            if (string.IsNullOrWhiteSpace(data.Country_Code2) || data==null)
                return BadRequest(new ApiResponse(500));
            var isPermanentlyBlocked = await _blockCountries.GetIsBlock(data.Country_Code2);
            var isTemporarilyBlocked = await _temporalBlockService.IsTemporarilyBlockedAsync(data.Country_Code2);
            bool isBlock = isPermanentlyBlocked || isTemporarilyBlocked;
            await _accessLog.Log(new AccessLog
            {
                Ip=ipAddress,
                Country_Code2=data.Country_Code2,
                IsBlock = isBlock,
                Timestamp=DateTime.UtcNow,
                UserAgent = Request.Headers["User-Agent"].ToString()

            });
            return Ok(new
            {
                ipAddress,
                countryCode = data.Country_Code2,
                isBlock
            });
        }

    }
}
