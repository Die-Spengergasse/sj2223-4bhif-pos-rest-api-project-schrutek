using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.SpengerShop.Application.Services.Customers.Queries;

namespace Spg.SpengerShop.Api.Controllers
{
    [Route("api/v{version:apiVersion}/Customer")]
    [ApiController]
    [ApiVersion("2.0")]
    public class BetterCustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BetterCustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery(id));
            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetFilteredCustomerQuery("C"));
            return Ok(result);
        }
    }
}
