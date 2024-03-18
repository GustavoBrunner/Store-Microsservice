using Microsoft.AspNetCore.Mvc;

namespace VshopWeb.Controllers;

public class ProductsController : Controller{

    public async Task<IActionResult> Index(){
        return View();
    }
}