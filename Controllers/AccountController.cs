using CatalogMVC.Models;
using CatalogMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogMVC.Controllers;

public class AccountController : Controller
{
    private readonly IAuthentication _authenticationService;

    public AccountController(IAuthentication authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpGet]
    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(UserViewModel userVM)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Login Inválido....");
            return View(userVM);
        }

        var result = await _authenticationService.UserAuthentication(userVM);

        if (result is null)
        {
            ModelState.AddModelError(string.Empty, "Login Inválido....");
            return View(userVM);
        }

        Response.Cookies.Append("X-Access-Token", result.Token, new CookieOptions()
        {
            Secure = true,
            HttpOnly = true,
            //SameSite = SameSiteMode.Strict
        });

        return Redirect("/");
    }

}
