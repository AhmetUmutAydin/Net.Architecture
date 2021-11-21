using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Net.Architecture.WebApi.Controllers.Common
{
    [Route("api/v1/personal-information")]
    [ApiController]
    public class PersonalInformationController : BaseController
    {
        private readonly IPersonalInformationHelper _personalInformationHelper;
        public PersonalInformationController(IPersonalInformationHelper personalInformationHelper)
        {
            _personalInformationHelper = personalInformationHelper;
        }

        [HttpGet("{deciderType}/{deciderId}")]
        [Authorize(Roles = "Company,Trainer,Demo")]
        [ProducesResponseType(typeof(PersonalInformationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PersonalInformationDto>> Get(long deciderType, long deciderId)
        {
            var result = await PersonalInformationFactory.CreateInstance(deciderType).GetPersonalInformation(deciderId);
            if (result.Result)
                return Ok(result.Data);
            else
                return BadRequest(result.BadRequest());
        }

        [HttpGet("personal-information-dropdown")]
        [Authorize(Roles = "Company,Trainer,Demo")]
        [ProducesResponseType(typeof(IEnumerable<DropdownDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<DropdownDto>>> GetPersonalInformationDropdown()
        {
            var result = await _personalInformationHelper.GetPersonalInformationDropdown();
            if (result.Result)
                return Ok(result.Data);
            else
                return BadRequest(result.BadRequest());
        }
    }
}
