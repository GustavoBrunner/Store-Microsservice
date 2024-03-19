using System.Collections;
using Microsoft.AspNetCore.Mvc;
using VshopWeb.Models;
using VshopWeb.Services.Interfaces;

namespace VshopWeb.Controllers;

public class ProductsController : Controller{

    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index(){
        var result = await _productService.GetAllProducts();
        
        if(result == null){
            return View("Error");
        }
        
        return View(result );
    }
}