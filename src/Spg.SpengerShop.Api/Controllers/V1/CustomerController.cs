using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Spg.SpengerShop.Application.Services.Customers.Commands;
using Spg.SpengerShop.Application.Services.Customers.Queries;
using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Domain.Filter;
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
        //private readonly IValidator<NewCustomerDto> _newCustomerValidator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
            //_newCustomerValidator = newCustomerValidator;
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

        [ValidationFilter()]
        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] NewCustomerDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(
                    new CreateCustomerCommand(
                        new Customer(Guid.NewGuid(), Genders.Male, 123, dto.FirstName, dto.LastName, dto.EMail, new DateTime(1977, 05, 13), DateTime.Now, new Address("x", "x", "x", "x"))));
                return Created("", result);
            }
            else
            {
                int i = ModelState.ErrorCount;
                return BadRequest(ModelState
                    .Where(m => m.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    ));
            }
        }

        [HttpPost("fluent")]
        public async Task<IActionResult> PostFluent([FromBody] NewCustomerDto dto)
        {
            Customer result = await _mediator.Send(
                new CreateCustomerCommand(
                    new Customer(Guid.NewGuid(), Genders.Male, 123, dto.FirstName, dto.LastName, dto.EMail, new DateTime(1977, 05, 13), DateTime.Now, new Address("x", "x", "x", "x"))));
            return Created("", result);

            //ValidationResult validationResult = await _newCustomerValidator.ValidateAsync(dto);
            //if (!validationResult.IsValid)
            //{
            //    return BadRequest(validationResult.ToDictionary());
            //}
            //Customer result = await _mediator.Send(new CreateCustomerCommand(new Customer(GenderTypes.MALE, dto.FirstName, dto.LastName, dto.EMail, new Guid(), DateTime.Now)));
            //return Created("", result);
        }
    }
}
