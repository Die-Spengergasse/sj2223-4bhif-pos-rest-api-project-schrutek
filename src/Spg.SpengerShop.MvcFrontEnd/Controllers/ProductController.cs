using Microsoft.AspNetCore.Mvc;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;

namespace Spg.SpengerShop.MvcFrontEnd.Controllers
{
    public class ProductController : Controller
    {
        private readonly IAddUpdateableProductService _addUpdateableProductService;
        private readonly IReadOnlyProductService _readOnlyProductService;

        public ProductController(IAddUpdateableProductService addUpdateableProductService, IReadOnlyProductService readOnlyProductService)
        {
            _addUpdateableProductService = addUpdateableProductService;
            _readOnlyProductService = readOnlyProductService;
        }

        public IActionResult Index()
        {
            //List<Product> model = _readOnlyProductService.GetAll().ToList();
            //return View(model);
            return View();
        }
    }
}
