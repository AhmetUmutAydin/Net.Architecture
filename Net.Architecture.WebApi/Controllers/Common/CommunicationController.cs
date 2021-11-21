using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.Architecture.WebApi.Controllers;
using Pilates.Application.Business.Helpers.Concrete;
using Pilates.Application.Business.ValidationRules.FluentValidation;
using Pilates.Application.Core.ActionsFilters;
using Pilates.Application.Core.Utilities.Result;
using Pilates.Application.Entities.Dtos;
using Pilates.Application.Entities.Enums;

namespace Pilates.Application.WebApi.Controllers.Common
{
    [Route("api/v1/communication")]
    [ApiController]
    public class CommunicationController : BaseController
    {
        [HttpGet("{deciderType}/{deciderId}")]
        [Authorize(Roles = "Company,Trainer,Demo")]
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
        [Authorize(Roles = "Company,Trainer")]
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
