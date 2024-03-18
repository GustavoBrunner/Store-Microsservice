using VshopWeb.Models;

namespace VshopWeb.Services.Interfaces;

public interface ICategoryService{

    Task<IEnumerable<CategoryViewModel>> GetAllCategories();
    Task<CategoryViewModel> GetCategoryById(int id);

    Task<CategoryViewModel> AddCategory(CategoryViewModel categoryViewModel);

    Task<CategoryViewModel> UpdateCategory(CategoryViewModel categoryViewModel);

    Task<CategoryViewModel> DeleteCategory(int id);

}