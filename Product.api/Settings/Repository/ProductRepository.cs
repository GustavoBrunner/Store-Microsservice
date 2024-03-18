using Microsoft.EntityFrameworkCore;
using ProductApi.Interfaces;
using ProductApi.Models;

namespace ProductApi.Settings;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _appContext;

    public ProductRepository(AppDbContext appContext){
        _appContext = appContext;
    }

    public async Task<bool> Add(ProductModel productModel)
    {
        await _appContext.Products.AddAsync(productModel);

        return await _appContext.SaveChangesAsync() > 0;
    }
    public async Task<bool> Remove(ProductModel productModel)
    {
        if(!CheckIfProductExists(productModel.ProductId)){
            return await _appContext.SaveChangesAsync() > 0;
        }
        _appContext.Products.Remove(productModel);

        return await _appContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(ProductModel productModel)
    {
        if(!CheckIfProductExists(productModel.ProductId)){
            return await _appContext.SaveChangesAsync() > 0;
        }

        _appContext.Entry(productModel).State = EntityState.Modified;
        return await _appContext.SaveChangesAsync() > 0;
    }

    public async Task<ProductModel> FindById(int id)
    {
        if(!CheckIfProductExists(id)){
            return null;
        }
        var product = await _appContext.Products
            .Where(p => p.ProductId == id)
                .Include(p => p.Category)
                    .FirstOrDefaultAsync();
                    
        return product;
    }

    public async Task<IEnumerable<ProductModel>> GetAll()
    {
        return await _appContext.Products
            .OrderByDescending(c => c.ProductId)
                .Include(p => p.Category)
                    .ToListAsync();
    }


    

    private bool CheckIfProductExists(int id){
        return _appContext.Products.Any(c => c.ProductId == id);
    }
}

