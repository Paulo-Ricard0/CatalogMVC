using CatalogMVC.Models;

namespace CatalogMVC.Services;

public interface IAuthentication
{
    Task<TokenViewModel> UserAuthentication(UserViewModel userVM);
}
