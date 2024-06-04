using System.ComponentModel.DataAnnotations;

namespace CatalogMVC.Models;

public class CategoryViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome da categoria é obrigatório")]
    [Display(Name = "Nome")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "A url da imagem é obrigatório")]
    [Display(Name = "Imagem")]
    public string? ImageUrl { get; set; }
}
