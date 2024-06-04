﻿using CatalogMVC.Models;

namespace CatalogMVC.Services;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetProducts(string token);
    Task<ProductViewModel> GetProductById(int id, string token);
    Task<ProductViewModel> CreateProduct(ProductViewModel productVM, string token);
    Task<bool> UpdateProduct(int id, ProductViewModel productVM, string token);
    Task<bool> DeleteProduct(int id, string token);
}
