using blocked_countries.DTO;
using blocked_countries.Errors;
using blocked_countries.Helper;
using blockedCountries.Core.Entities;
using blockedCountries.Core.Repositories;
using blockedCountries.Core.Specification;
using blockedCountries.Core.Specification.AccessLogSpec;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace blocked_countries.Controllers
{
    public class LogController : BaseApiController
    {
        private readonly IAccessLog _accessLog;

        public LogController(IAccessLog accessLog)
        {
            _accessLog = accessLog;
        }
        [HttpGet("blocked-attempts")]
        public async Task<ActionResult<AccessLog>> GetLogs([FromQuery]SpecPrams spec)
        {
            var specification= new AccessSpecSortAndPagination(spec);
            var Data= await _accessLog.GetBlockLog(specification);
            var countSpec = new AccessLogFilter(spec);
            var count = await _accessLog.GetCountAsync(countSpec);
            if (count == 0)
                return NotFound(new ApiResponse(404));
            return Ok(new Pagination<AccessLog>(spec.PageIndex, spec.PageSize, count, Data));

        }
    }
}
