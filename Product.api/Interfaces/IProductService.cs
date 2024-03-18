using ProductApi.Models;

namespace ProductApi.Interfaces;

public interface IProductService {
    //search operations
    Task<ProductModelDto> FindById(int id);

    Task<IEnumerable<ProductModelDto>> GetAll();

    Task<CategoryModelDto> GetCategoryFromProduct(int id);

    //manipulation operations
    Task Add(ProductModelDto productModel);

    Task Remove(int id);

    Task Update(ProductModelDto productModel);
}