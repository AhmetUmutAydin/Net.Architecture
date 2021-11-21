using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.Architecture.Business.Helpers.Concrete;
using Net.Architecture.Business.ValidationRules.FluentValidation;
using Net.Architecture.Core.ActionsFilters;
using Net.Architecture.Core.Utilities.Result;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.WebApi.Controllers.Common
{
    [Route("api/v1/communication")]
    [ApiController]
    public class CommunicationController : BaseController
    {
        [HttpGet("{deciderType}/{deciderId}")]
        [Authorize(Roles = "")]
        [ProducesResponseType(typeof(CommunicationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CommunicationDto>> Get(long deciderType, long deciderId)
        {


            var result = await CommunicationFactory.CreateInstance(deciderType).GetCommunication(deciderId);
            if (result.Result)
                return Ok(result.Data);
            else
                return BadRequest(result.BadRequest());
        }

        [HttpPost]
        [Authorize(Roles = "")]
        [DbTransaction]
        [Validation(typeof(CommunicationDtoValidator))]
        [ProducesResponseType(typeof(IEnumerable<CommunicationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CommunicationDto>>> Post(ContactDto contact)
        {


            var result = await CommunicationFactory.CreateInstance(contact.Decider.DeciderType).SaveCommunication(contact);
            if (!result.Result)
            {
                return BadRequest(result.BadRequest());
            }
            return Ok(result.Data);
        }
    }
}
