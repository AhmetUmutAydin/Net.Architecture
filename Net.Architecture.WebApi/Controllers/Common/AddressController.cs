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
    [Route("api/v1/address")]
    [ApiController]
    public class AddressController : BaseController
    {
        [HttpGet("{deciderType}/{deciderId}")]
        [Authorize(Roles = "")]
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
        [Authorize(Roles = "")]
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
        [Authorize(Roles = "")]
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
