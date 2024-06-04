using CatalogMVC.Models;
using CatalogMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private string token = string.Empty;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
        {
            //extrai o token do cookie
            var result = await _productService.GetProducts(GetJwtToken());

            if (result is null)
                return View("Error");

            return View(result);
        }

        private string GetJwtToken()
        {
            if (HttpContext.Request.Cookies.ContainsKey("X-Access-Token"))
                token = HttpContext.Request.Cookies["X-Access-Token"].ToString();

            return token;
        }
    }
}
