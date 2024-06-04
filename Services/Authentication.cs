using CatalogMVC.Models;
using System.Text;
using System.Text.Json;

namespace CatalogMVC.Services;

public class Authentication : IAuthentication
{
    const string apiEndpointAuth = "api/v1/Auth/Login/";
    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _options;
    private TokenViewModel userToken;

    public Authentication(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }


    public async Task<TokenViewModel> UserAuthentication(UserViewModel userVM)
    {
        var client = _clientFactory.CreateClient("CatalogApiClient");
        var user = JsonSerializer.Serialize(userVM);
        StringContent content = new StringContent(user, Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpointAuth, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                userToken = await JsonSerializer
                    .DeserializeAsync<TokenViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return userToken;
    }
}
