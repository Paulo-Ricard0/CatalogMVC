using Microsoft.AspNetCore.Mvc;

namespace CatalogMVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}