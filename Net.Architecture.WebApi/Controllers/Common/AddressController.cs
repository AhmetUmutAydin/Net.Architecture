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


namespace Pilates.Application.WebApi.Controllers.Common
{
    [Route("api/v1/address")]
    [ApiController]
    public class AddressController : BaseController
    {
        [HttpGet("{deciderType}/{deciderId}")]
        [Authorize(Roles = "Company,Trainer,Demo")]
        [ProducesResponseType(typeof(AddressDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AddressDto>> Get(long deciderType, long deciderId)
        {
            var result = await AddressFactory.CreateInstance(deciderType).GetAddress(deciderId);
            if (result.Result)
                return Ok(result.Data);
            else
                return BadRequest(result.BadRequest());
        }

        [HttpPost]
        [Authorize(Roles = "Company,Trainer")]
        [DbTransaction]
        [Validation(typeof(AddressDtoValidator))]
        [ProducesResponseType(typeof(AddressDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(AddressDto address)
        {
            var result = await AddressFactory.CreateInstance(address.Decider.DeciderType).InsertAddress(address);
            if (!result.Result)
            {
                return BadRequest(result.BadRequest());
            }
            return CreatedAtAction("Get", new { deciderType = address.Decider.DeciderType, deciderId = address.Decider.DeciderId }, result.Data);
        }

        [HttpPut]
        [Authorize(Roles = "Company,Trainer")]
        [DbTransaction]
        [Validation(typeof(AddressDtoValidator))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IServiceResult), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(AddressDto address)
        {
            var result = await AddressFactory.CreateInstance(address.Decider.DeciderType).UpdateAddress(address);
            if (result.Result)
                return NoContent();
            else
                return NotFound(result.NotFound());
        }

    }
}
