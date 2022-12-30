using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ESHOP.Models;
using ESHOP.Services.Interfaces;

namespace ESHOP.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly IOrderService _orderService;

    public HomeController(ILogger<HomeController> logger,
        IProductService productService, IOrderService orderService)
    {
        _productService = productService;
        _orderService = orderService;
    }

    public IActionResult Index()
    {
        var products = _productService.GetAllProducts();
        return View(products);
    }

    [Route("ContactUs")]
    public IActionResult ContactUs() => View();
    
    public IActionResult Detail(int id)
    {
        var product = _productService.GetProductIncludeItem(id);

        if (product == null)
            return NotFound();

        var categories = _productService.GetProductCategories(id);

        var viewModel = new DetailViewModel()
        {
            Product = product,
            Categories = categories
        };

        return View(viewModel);
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}