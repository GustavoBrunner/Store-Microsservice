using Microsoft.EntityFrameworkCore;
using ProductApi.Interfaces;
using ProductApi.Models;

namespace ProductApi.Settings;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _appContext;

    public CategoryRepository(AppDbContext appContext){
        _appContext = appContext;
    }
    public async Task<bool> Add(CategoryModel categoryModel)
    {
        await _appContext.Categories.AddAsync(categoryModel);

        return await _appContext.SaveChangesAsync() > 0;
    }
    public async Task<bool> Remove(int id)
    {
        var category = FindById(id);
        _appContext.Categories.Remove(category.Result);

        return await _appContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(CategoryModel categoryModel)
    {
        if(!CheckIfCategoryExists(categoryModel.CategoryId)){
            return await _appContext.SaveChangesAsync() > 0;
        }

        _appContext.Entry(categoryModel).State = EntityState.Modified;
        return await _appContext.SaveChangesAsync() > 0;
    }

    public async Task<CategoryModel> FindById(int id)
    {
        if(!CheckIfCategoryExists(id)){
            return null;
        }
        var category = await _appContext.Categories.FindAsync(id);
        return category;
    }

    public async Task<IEnumerable<CategoryModel>> GetAll()
    {
        return await _appContext.Categories
            .OrderByDescending(c => c.CategoryId)
                .ToListAsync();
    }

    public async Task<IEnumerable<CategoryModel>> GetAllProducts()
    {
        return await _appContext.Categories
            .Include(c => c.Products)
                .ToListAsync();
    }

    public async Task<IEnumerable<ProductModel>> GetProductFromCategory(int id)
    {
        if(!CheckIfCategoryExists(id)){
            return null;
        }

        var category = _appContext.Categories.FindAsync(id);

        return category.Result.Products.ToList();
    }

    private bool CheckIfCategoryExists(int id){
        return _appContext.Categories.Any(c => c.CategoryId == id);
    }

}