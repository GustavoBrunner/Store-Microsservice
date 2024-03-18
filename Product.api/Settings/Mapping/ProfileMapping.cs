using AutoMapper;
using ProductApi.Models;

namespace ProductApi.Settings;

public class ProfileMapping : Profile{

    /* Mapeamento de entidades para DTOS, o automapper pega os atributos das entidades
    e cria o dto correspondente a elas, e vice-versa */
    public ProfileMapping()
    {
        CreateMap<CategoryModel, CategoryModelDto>().ReverseMap();
        /* define the mapping of a attribute to product.categoryName, passing options.MapFrom
        getting, then, the Category field name */
        CreateMap<ProductModel, ProductModelDto>()
            .ForMember(p => p.CategoryName, 
                opt => opt.MapFrom(pm => pm.Category.Name));

    }
}