using ProductApi.Models;

namespace ProductApi.Interfaces;


/* all the database transactions are made by DTO, so we need a Service to make the mapping
from a DTO object to a entity object*/
public interface ICategoryService{
    //search operations
    Task<CategoryModelDto> FindById(int id);

    Task<IEnumerable<CategoryModelDto>> GetAll();

    Task<IEnumerable<ProductModelDto>> GetProductFromCategory(int id);

    //manipulation operations
    Task Add(CategoryModelDto categoryModel);

    Task Remove(int id);

    Task Update(CategoryModelDto categoryModel);
}