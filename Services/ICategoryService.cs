using CatalogMVC.Models;

namespace CatalogMVC.Services;

public interface ICategoryService
{
    Task<CategoryViewModel> CreateCategory(CategoryViewModel categoriaVM);
    Task<IEnumerable<CategoryViewModel>> GetCategories();
    Task<CategoryViewModel> GetCategoryById(int id);
    Task<bool> UpdateCategory(int id, CategoryViewModel categoriaVM);
    Task<bool> DeleteCategory(int id);
}
