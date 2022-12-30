using ESHOP.Models;
using ESHOP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ESHOP.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        public IndexModel(IProductService productService) => _productService = productService;

        public IEnumerable<Product> Products { get; set; }
        
        public void OnGet() => Products = _productService.GetProductsIncludeItems();
        public void OnPost() { }


    }
}
