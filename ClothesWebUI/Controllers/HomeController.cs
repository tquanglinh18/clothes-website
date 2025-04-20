using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClothesWebUI.Models;
using ClothesWebUI.LibsServices;

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
        lstProducts = await _productService.GetAllProducts();

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

    public async Task<IActionResult> Edit(int id)
    {
        var prodEdit = await _productService.GetProductById(id);
        return View(prodEdit.Data);
    }


    [HttpGet]
    public IActionResult Insert()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Insert([FromForm] Products model)
    {
        if (ModelState.IsValid)
        {
            // Lưu dữ liệu vào DB ở đây

            return RedirectToAction("Index");
        }

        // Nếu có lỗi validation thì trả lại view với dữ liệu cũ
        return View("Insert",model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

