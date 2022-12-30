using ESHOP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ESHOP.Controllers;

public class ProductController : Controller
{
    IProductService _productService;

    public ProductController(IProductService productService) => _productService = productService;

    [Route("/Groups/{id}/{name}")]
    public IActionResult ShowProductsByGroupId(int id,  string name)
    {
        ViewData["GroupName"] = name;
        var products = _productService.GetCategoryProducts(id);
        return View(products);
    }
}