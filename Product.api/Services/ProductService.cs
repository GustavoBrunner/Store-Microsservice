using AutoMapper;
using ProductApi.Interfaces;
using ProductApi.Models;

namespace ProductApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public ProductService(IProductRepository productRepository, IMapper mapper){
        _productRepository = productRepository;
        _mapper = mapper;
    }


    public async Task<ProductModelDto> FindById(int id)
    {
        //pega uma entidade no banco de dados e retorna pelo id
        var product = await _productRepository.FindById(id);
        //transforma essa entidade em um DTO de product model
        return _mapper.Map<ProductModelDto>(product);
    }


    public async Task Remove(int id)
    {
        var product = _productRepository.FindById(id);

        if(product != null) { await _productRepository.Remove(product.Result);}
    }

    public async Task<CategoryModelDto> GetCategoryFromProduct(int id)
    {
        var product = await _productRepository.FindById(id);
        var category = product.Category;
        return _mapper.Map<CategoryModelDto>(category);
    }
    public async Task<IEnumerable<ProductModelDto>> GetAll()
    {
        var products = await _productRepository.GetAll();
        return _mapper.Map<IEnumerable<ProductModelDto>>(products);
    }

    public async Task Update(ProductModelDto productModelDto)
    {
        var product = _mapper.Map<ProductModel>(productModelDto);
        await _productRepository.Update(product);
    }
    public async Task Add(ProductModelDto productModel)
    {
        var product = _mapper.Map<ProductModel>(productModel);
        await _productRepository.Add(product);
    }

}