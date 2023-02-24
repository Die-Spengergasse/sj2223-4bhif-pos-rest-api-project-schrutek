using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.SpengerShop.Application.Filter;
using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;

namespace Spg.SpengerShop.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ProductsController : ControllerBase
    {
        private readonly IReadOnlyProductService _readOnlyProductService;
        private readonly IValidator<NewProductDto> _validator;

        public ProductsController(IReadOnlyProductService readOnlyProductService, IValidator<NewProductDto> validator)
        {
            _readOnlyProductService = readOnlyProductService;
            _validator = validator;
        }

        /// <summary>
        /// Gibt alle Produkte aus der Datenbank zurück.
        /// </summary>
        /// <returns>Eine Liste aller Produkte</returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<ProductDto> result = _readOnlyProductService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Hier logging einfügen...
                return BadRequest();
            }
        }

        [HttpPost()]
        [Produces("application/json")]
        [HasRole()]
        public IActionResult Save([FromBody()] NewProductDto newProduct) 
        {
            // bad coding
            //if (string.IsNullOrEmpty(newProduct.Name))
            //{
            //    return BadRequest();
            //}
            //if (newProduct.Name.Length == 0)
            //{
            //    return BadRequest();
            //}
            //if (newProduct.Name.Length > 20)
            //{
            //    return BadRequest();
            //}
            // ...

            // TODO: Create to DB and return 201

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}
            ValidationResult result = _validator.Validate(newProduct);
            if (result.IsValid)
            { }

            return Created("url", null);
        }
    }
}
