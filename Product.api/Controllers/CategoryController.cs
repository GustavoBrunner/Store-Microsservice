using System.Collections;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Interfaces;
using ProductApi.Models;

namespace ProductApi.Controllers;


/* Essas anotações definem que essa é a rota de uma API, e que essa classe se trata de um
controlador de API (Application Programming Interface (Interface de Programação de Aplicação)) */
[Route("/api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase{

    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    //indica o método get padrão do controlador
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryModelDto>>> Get(){
        var categoriesDto = await _categoryService.GetAll();
        if(categoriesDto is null) { return NotFound("Categories not found"); }


        return Ok(categoriesDto);
    } 
    //route: api/category/id
    [HttpGet("{id:int}", Name ="GetCategory")]
    public async Task<ActionResult<CategoryModelDto>> GetSingleCategory(int id){
        var category = await _categoryService.FindById(id);
        if(category is null) { return NotFound("Category not found"); }

        return Ok(category);
    }
    //route: api/category/products/id
    [HttpGet("products/{id:int}")]
    public async Task<ActionResult<IEnumerable<ProductModelDto>>> GetCategoryProduts(int id){
        var products = await _categoryService.GetProductFromCategory(id);
        if(products is null) { return NotFound("No products listed"); }

        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryModel>> AddCategory([FromBody] CategoryModelDto categoryModelDto){
        //verify if the post data is null. If true, return a bad request
        if(categoryModelDto is null) { return BadRequest("Invalid Data"); }

        //add the data to the repository and the data base
        await _categoryService.Add(categoryModelDto);

        //create a new route result passing the name chosed on the get method, and a new id
        //its the same as passing a route in the url on the browser
        return new CreatedAtRouteResult("GetCategory", new { id = categoryModelDto.CategoryId}, 
            categoryModelDto);

    }
    //"id:int" significa que está sendo colocada uma restrição no id, ele PRECISA ser um int.
    [HttpPut("id:int")]
    public async Task<ActionResult> UpdateCategory(int id, [FromBody] CategoryModelDto categoryDto){
        if(id != categoryDto.CategoryId) { return BadRequest(); }

        if(categoryDto is null) { return BadRequest(); }

        await _categoryService.Update(categoryDto);

        return Ok(categoryDto);
    }

    //http method to delete actions
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id){
        var categoryDto = await _categoryService.FindById(id);

        if(categoryDto == null) { return NoContent(); }

        await _categoryService.Remove(id);

        return Ok(categoryDto);
    }
}
