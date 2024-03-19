using Microsoft.AspNetCore.Mvc;

namespace VshopWeb.Controllers;

public class HomeController : Controller{
    public IActionResult Index(){
        return View();
    }
}