using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.Architecture.Business.Abstract.Common;
using Net.Architecture.Core.Utilities.Result;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.WebApi.Controllers.Common
{
    [Route("api/v1/common")]
    [ApiController]
    public class CommonController : BaseController
    {
        private readonly IDomainEnumService _domainEnumManager;
        private readonly ICityService _cityService;
        private readonly IDistrictService _districtService;

        public CommonController(IDomainEnumService domainEnumManager, IDistrictService districtService, ICityService cityService)
        {
            _domainEnumManager = domainEnumManager;
            _districtService = districtService;
            _cityService = cityService;
        }

        [HttpGet("{parentId}")]
        [Authorize(Roles = "")]
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
        [Authorize(Roles = "")]
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
        [Authorize(Roles = "")]
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
    }
}
