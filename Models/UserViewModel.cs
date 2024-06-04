using System.ComponentModel.DataAnnotations;

namespace CatalogMVC.Models;

public class UserViewModel
{
    [Display(Name = "Username")]

    public string? UserName { get; set; }

    [Display(Name = "Senha")]
    public string? Password { get; set; }
}
