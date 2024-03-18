using AutoMapper;
using ProductApi.Interfaces;
using ProductApi.Models;

namespace ProductApi.Services;

/* all the database transactions are made by DTO, so we need a Service to make the mapping
from a DTO object to a entity object*/
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper){
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    
    public async Task Add(CategoryModelDto categoryModel)
    {
        var category = _mapper.Map<CategoryModel>(categoryModel);
        await _categoryRepository.Add(category);
    }
    public async Task Update(CategoryModelDto categoryModel)
    {
        var category = _mapper.Map<CategoryModel>(categoryModel);
        await _categoryRepository.Update(category);
    }
    public async Task Remove(int id)
    {
        await _categoryRepository.Remove(id);        
    }

    public async Task<CategoryModelDto> FindById(int id)
    {
        var category = await _categoryRepository.FindById(id);
        return _mapper.Map<CategoryModelDto>(category);
    }

    public async Task<IEnumerable<CategoryModelDto>> GetAll()
    {
        var categories = await _categoryRepository.GetAll();
        return _mapper.Map<IEnumerable<CategoryModelDto>>(categories);
    }

    public async Task<IEnumerable<ProductModelDto>> GetProductFromCategory(int id)
    {
        var category = await _categoryRepository.FindById(id);
        var products = category.Products;
        return _mapper.Map<IEnumerable<ProductModelDto>>(products);
    }

}