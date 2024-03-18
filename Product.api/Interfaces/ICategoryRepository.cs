

using System.Collections;
using ProductApi.Models;

namespace ProductApi.Interfaces;

public interface ICategoryRepository{
    //search operations
    Task<CategoryModel> FindById(int id);

    Task<IEnumerable<CategoryModel>> GetAll();

    Task<IEnumerable<CategoryModel>> GetAllProducts();

    Task<IEnumerable<ProductModel>> GetProductFromCategory(int id);

    //manipulation operations
    Task<bool> Add(CategoryModel categoryModel);

    Task<bool> Remove(int id);

    Task<bool> Update(CategoryModel categoryModel);
}