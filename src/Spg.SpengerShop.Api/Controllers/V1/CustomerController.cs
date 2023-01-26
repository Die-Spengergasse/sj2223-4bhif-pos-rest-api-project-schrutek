using MediatR;
using Microsoft.AspNetCore.Mvc;
using Spg.SpengerShop.Application.Services.Customers.Commands;
using Spg.SpengerShop.Application.Services.Customers.Queries;
using Spg.SpengerShop.Domain.Model;
using System.Linq.Expressions;

namespace Spg.SpengerShop.Api.Controllers.V1
{
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/Customer")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
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
            var result = await _mediator.Send(new GetFilteredCustomerQuery("M"));
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] Customer dto)
        {
            var result = await _mediator.Send(new CreateCustomerCommand(dto));
            return Created("", result);
        }
    }
}
