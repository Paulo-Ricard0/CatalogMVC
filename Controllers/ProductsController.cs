using CatalogMVC.Models;
using CatalogMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [HttpGet]
        public async Task<IActionResult> ShowViewCreateProduct()
        {
            ViewBag.Id = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            return View("CreateNewProduct");
        }

        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> CreateNewProduct(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProduct(productVM, GetJwtToken());

                if (result != null)
                    return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Id = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            }
            return View(productVM);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await _productService.GetProductById(id, GetJwtToken());

            if (product is null)
                return View("Error");

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> ShowViewUpdateProduct(int id)
        {
            var result = await _productService.GetProductById(id, GetJwtToken());

            if (result is null)
                return View("Error");

            ViewBag.Id = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            return View("UpdateProduct", result);
        }

        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> UpdateProduct(int id, ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.UpdateProduct(id, productVM, GetJwtToken());

                if (result)
                    return RedirectToAction(nameof(Index));
            }
            return View(productVM);
        }

        [HttpGet]
        public async Task<ActionResult> ShowViewDeleteProduct(int id)
        {
            var result = await _productService.GetProductById(id, GetJwtToken());

            if (result is null)
                return View("Error");

            return View("DeleteProduct", result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id, GetJwtToken());

            if (result)
                return RedirectToAction("Index");

            return View("Error");
        }

        private string GetJwtToken()
        {
            if (HttpContext.Request.Cookies.ContainsKey("X-Access-Token"))
                token = HttpContext.Request.Cookies["X-Access-Token"].ToString();

            return token;
        }
    }
}
