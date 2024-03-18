using ProductApi.Models;

namespace ProductApi.Interfaces;

public interface IProductRepository{
    //search operations
    Task<ProductModel> FindById(int id);

    Task<IEnumerable<ProductModel>> GetAll();

    

    //manipulation operations
    Task<bool> Add(ProductModel categoryModel);

    Task<bool> Remove(ProductModel categoryModel);

    Task<bool> Update(ProductModel categoryModel);
}