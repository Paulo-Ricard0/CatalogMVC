using System.ComponentModel.DataAnnotations;

namespace CatalogMVC.Models;

public class ProductViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do produto é obrigatório")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "A descrição do produto é obrigatória")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Informe o preço do produto")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Informe o caminho da imagem do produto")]
    [Display(Name = "Caminho da Imagem")]
    public string? ImageUrl { get; set; }

    [Display(Name = "Categoria")]
    public int CategoryID { get; set; }
}
