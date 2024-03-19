using System.Collections;
using VshopWeb.Models;

namespace VshopWeb.Services.Interfaces;

public interface IProductService{
    Task<IEnumerable<ProductViewModel>> GetAllProducts();

    Task<ProductViewModel> GetProductById(int id);

    Task<ProductViewModel> UpdateProduct(ProductViewModel productViewModel);

    Task<ProductViewModel> CreateProduct(ProductViewModel productViewModel);

    Task<bool> DeleteProductById(int id);
}