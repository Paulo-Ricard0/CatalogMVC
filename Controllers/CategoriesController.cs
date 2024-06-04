using CatalogMVC.Models;
using CatalogMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogMVC.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryViewModel>>> Index()
    {
        var result = await _categoryService.GetCategories();

        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpGet]
    public IActionResult ShowViewCreateCategory()
    {
        return View("CreateNewCategory");
    }

    [HttpPost]
    public async Task<ActionResult<CategoryViewModel>> CreateNewCategory(CategoryViewModel categoryVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _categoryService.CreateCategory(categoryVM);

            if (result != null)
                return RedirectToAction(nameof(Index));
        }

        ViewBag.Erro = "Erro ao criar Categoria";
        return View(categoryVM);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoryIdAndReturnUpdateView(int id)
    {
        var result = await _categoryService.GetCategoryById(id);

        if (result is null)
            return View("Error");

        return View("UpdateCategory", result);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryViewModel>> UpdateCategory(int id, CategoryViewModel categoryVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _categoryService.UpdateCategory(id, categoryVM);

            if (result)
                return RedirectToAction(nameof(Index));
        }
        ViewBag.Erro = "Erro ao atualizar Categoria";
        return View(categoryVM);
    }

    [HttpGet]
    public async Task<ActionResult> GetCategoryIdAndReturnDeleteView(int id)
    {
        var result = await _categoryService.GetCategoryById(id);

        if (result is null)
            return View("Error");

        return View("DeleteCategory", result);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var result = await _categoryService.DeleteCategory(id);

        if (result)
            return RedirectToAction("Index");

        return View("Error");
    }
}
