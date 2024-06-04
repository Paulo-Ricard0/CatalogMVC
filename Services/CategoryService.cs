using CatalogMVC.Models;
using System.Text;
using System.Text.Json;

namespace CatalogMVC.Services;

public class CategoryService : ICategoryService
{
    private const string apiEndpoint = "/api/v1/Categories/";
    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _clientFactory;

    private CategoryViewModel categoryVM = new CategoryViewModel();
    private IEnumerable<CategoryViewModel> categoriesVM = new List<CategoryViewModel>();

    public CategoryService(IHttpClientFactory clientFactory)
    {
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _clientFactory = clientFactory;
    }

    // ACTIONS
    public async Task<CategoryViewModel> CreateCategory(CategoryViewModel categoryVM)
    {
        var client = _clientFactory.CreateClient("CatalogApiClient");
        var category = JsonSerializer.Serialize(categoryVM);
        StringContent content = new StringContent(category, Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                categoryVM = await JsonSerializer
                    .DeserializeAsync<CategoryViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return categoryVM;
    }

    public async Task<IEnumerable<CategoryViewModel>> GetCategories()
    {
        var client = _clientFactory.CreateClient("CatalogApiClient");

        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                categoriesVM = await JsonSerializer
                    .DeserializeAsync<IEnumerable<CategoryViewModel>>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return categoriesVM;
    }

    public async Task<CategoryViewModel> GetCategoryById(int id)
    {
        var client = _clientFactory.CreateClient("CatalogApiClient");

        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                categoryVM = await JsonSerializer
                    .DeserializeAsync<CategoryViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return categoryVM;
    }

    public async Task<bool> UpdateCategory(int id, CategoryViewModel categoryVM)
    {
        var client = _clientFactory.CreateClient("CatalogApiClient");

        using var response = await client.PutAsJsonAsync(apiEndpoint + id, categoryVM);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCategory(int id)
    {
        var client = _clientFactory.CreateClient("CatalogApiClient");

        using var response = await client.DeleteAsync(apiEndpoint + id);
        return response.IsSuccessStatusCode;
    }
}
