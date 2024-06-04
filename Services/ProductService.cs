﻿using CatalogMVC.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CatalogMVC.Services;

public class ProductService : IProductService
{
    private const string apiEndpoint = "api/v1/Products/";
    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _options;

    private ProductViewModel productVM;
    private IEnumerable<ProductViewModel> productsVM;

    public ProductService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<ProductViewModel> CreateProduct(ProductViewModel productVM, string token)
    {
        var client = _clientFactory.CreateClient("CatalogApiClient");
        PutTokenInHeaderAuthorization(token, client);

        var product = JsonSerializer.Serialize(productVM);
        StringContent content = new StringContent(product, Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                productVM = await JsonSerializer
                               .DeserializeAsync<ProductViewModel>
                               (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return productVM;
    }

    public async Task<IEnumerable<ProductViewModel>> GetProducts(string token)
    {
        var client = _clientFactory.CreateClient("CatalogApiClient");
        PutTokenInHeaderAuthorization(token, client);

        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                productsVM = await JsonSerializer
                               .DeserializeAsync<IEnumerable<ProductViewModel>>
                               (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return productsVM;
    }

    public async Task<ProductViewModel> GetProductById(int id, string token)
    {
        var client = _clientFactory.CreateClient("CatalogApiClient");
        PutTokenInHeaderAuthorization(token, client);

        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                productVM = await JsonSerializer
                               .DeserializeAsync<ProductViewModel>
                               (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return productVM;
    }

    public async Task<bool> UpdateProduct(int id, ProductViewModel productVM, string token)
    {
        var client = _clientFactory.CreateClient("CatalogApiClient");
        PutTokenInHeaderAuthorization(token, client);

        using (var response = await client.PutAsJsonAsync(apiEndpoint + id, productVM))
        {
            return response.IsSuccessStatusCode;
        }
    }

    public async Task<bool> DeleteProduct(int id, string token)
    {
        var client = _clientFactory.CreateClient("CatalogApiClient");
        PutTokenInHeaderAuthorization(token, client);

        using (var response = await client.DeleteAsync(apiEndpoint + id))
        {
            return response.IsSuccessStatusCode;
        }
    }

    private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
