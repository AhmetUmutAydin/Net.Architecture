using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Net.Architecture.WebApi.Controllers.Common
{
    [Route("api/v1/common")]
    [ApiController]
    public class CommonController : BaseController
    {
        private readonly IDomainEnumService _domainEnumManager;
        private readonly ICityService _cityService;
        private readonly IDistrictService _districtService;
        private readonly IStatisticsService _statisticsService;

        public CommonController(IDomainEnumService domainEnumManager, IDistrictService districtService, ICityService cityService, IStatisticsService statisticsService)
        {
            _domainEnumManager = domainEnumManager;
            _districtService = districtService;
            _cityService = cityService;
            _statisticsService = statisticsService;
        }

        [HttpGet("{parentId}")]
        [ProducesResponseType(typeof(IEnumerable<DropdownDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<DropdownDto>>> Get(long parentId)
        {
            var result = await _domainEnumManager.GetChildEnums(parentId);
            if (result.Result)
                return Ok(result.Data);
            else
                return BadRequest(result.BadRequest());
        }

        [HttpGet("cities")]
        [Authorize(Roles = "Company,Trainer,Admin,Demo")]
        [ProducesResponseType(typeof(IEnumerable<DropdownDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<DropdownDto>>> Cities()
        {
            var result = await _cityService.GetCities();
            if (result.Result)
                return Ok(result.Data);
            else
                return BadRequest(result.BadRequest());
        }

        [HttpGet("districts/{cityId}")]
        [Authorize(Roles = "Company,Trainer,Admin,Demo")]
        [ProducesResponseType(typeof(IEnumerable<DropdownDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<DropdownDto>>> Districts(long cityId)
        {
            var result = await _districtService.GetDistricts(cityId);
            if (result.Result)
                return Ok(result.Data);
            else
                return BadRequest(result.BadRequest());
        }

        //#Todo Validation
        [HttpPost("dashboard")]
        [Authorize(Roles = "Company,Trainer,Admin,Demo")]
        [ProducesResponseType(typeof(DashboardDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DashboardDto>> DashboardStatistics(FilterStatisticsDto filterStatisticsDto)
        {
            var result = await _statisticsService.GetDashboardStatistics(filterStatisticsDto);
            if (result.Result)
                return Ok(result.Data);
            else
                return BadRequest(result.BadRequest());
        }
    }
}
