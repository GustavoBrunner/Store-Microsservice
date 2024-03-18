
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Interfaces;
using ProductApi.Models;

namespace ProductApi.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class ProductController : ControllerBase{
    
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductModelDto>>> Get(){
        var products = await _productService.GetAll();

        if(products is null) { return NotFound("No products found to list"); }

        return Ok(products);
    }
    [HttpGet("{id:int}", Name = "GetProduct")]
    public async Task<ActionResult<ProductModelDto>> GetSingleProduct(int id){
        var product = _productService.FindById(id);
        if(product is null) { return NotFound("Product not found"); }

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromBody] ProductModelDto productModelDto){
        if(productModelDto is null) { return BadRequest(); }

        await _productService.Add(productModelDto);

        return new CreatedAtRouteResult("GetProduct", new { id = productModelDto.ProductId},
            productModelDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int? id, [FromBody] ProductModelDto productModelDto){
        if(id is null) { return NotFound("Product not found!"); }

        if(productModelDto is null) { return BadRequest("Invalid data"); }

        await _productService.Update(productModelDto);

        return Ok(productModelDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int? id){
        if(id is null) { return NotFound(); }

        var product = await _productService.FindById(id.Value);
        
        if(product == null) { return NoContent(); }

        await _productService.Remove(id.Value);

        return Ok(product);

    }
}
