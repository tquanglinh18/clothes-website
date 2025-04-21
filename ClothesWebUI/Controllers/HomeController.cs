using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClothesWebUI.Models;
using ClothesWebUI.LibsServices;
using System.IO.Pipelines;
using ClothesWebUI.Constants;
using System.Reflection;

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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditPost([FromForm] Products prod)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.UpdateProduct(prod);

            if (response != null && response.IsSuccessed && response.Code == (int)ResponseCode.OK)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", response?.Message ?? "Cập nhật thất bại!");
            }
        }

        return View("Edit", prod);
    }

    [HttpGet]
    public IActionResult Insert()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> InsertPost([FromForm] Products model)
    {
        if (ModelState.IsValid)
        {
            var prodInput = new Products
            {
                ProductName = model.ProductName,
                Description = model.Description,
                Price = model.Price,
                Quantity = model.Quantity,
            };

            var response = await _productService.InsertProduct(prodInput);

            if (response != null && response.IsSuccessed && response.Code == (int)ResponseCode.OK)
            {

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", response?.Message ?? "Lỗi không xác định!");
            }
        }
        return View("Insert", model);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var temp = await _productService.GetProductById(id);

        if (temp != null)
        {
            await _productService.DeleteProduct(id);
        }

        lstProducts = await _productService.GetAllProducts();

        if (!lstProducts.IsSuccessed)
        {
            ViewBag.Error = lstProducts.Message;
            return RedirectToAction("Error");
        }

        return RedirectToAction("Index");

    }

    public async Task<IActionResult> Detail(int id)
    {
        var result = await _productService.GetProductById(id);

        if (result.Code != (int)ResponseCode.OK)
        {
            return Error();
        }

        return View(result.Data);

    }

    [HttpGet]
    public async Task<IActionResult> SearchByName(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            ModelState.AddModelError("", "Vui lòng nhập từ khóa tìm kiếm.");
            var result = await _productService.GetAllProducts();
            return View("Index", result.Data);
        }

        var searchResult = await _productService.SearchByName(keyword);

        if (searchResult.IsSuccessed && searchResult.Data != null && searchResult.Data.Any())
        {
            ViewBag.SearchKeyword = keyword;
            return View("Index", searchResult.Data);
        }

        ViewBag.Error = "Không tìm thấy sản phẩm nào phù hợp.";
        return View("Index", new List<Products>());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

