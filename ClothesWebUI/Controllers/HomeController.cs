using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClothesWebUI.Models;
using ClothesWebUI.LibsServices;
using ClothesWebAPI.Models;

namespace ClothesWebUI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public ResponseModels<List<Products>> lstProducts;

    private readonly IProductService _productService;

    public HomeController(ILogger<HomeController> logger, IProductService _prodService)
    {
        _logger = logger;
        _productService = _prodService;
    }

    public async Task<IActionResult> IndexAsync()
    {
        lstProducts = await _productService.GetAllProductsAsync();

        if (!lstProducts.IsSuccessed)
        {
            ViewBag.Error = lstProducts.Message;
            return View(new List<Products>());
        }

        return View(lstProducts.Data);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

