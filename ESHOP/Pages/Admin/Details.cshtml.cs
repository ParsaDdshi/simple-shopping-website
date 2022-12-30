using ESHOP.Models;
using ESHOP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ESHOP.Pages.Admin
{
    public class DetailsModel : PageModel
    {
        private readonly IProductService _productService;
        public DetailsModel(IProductService productService) => _productService = productService;
        

        public ProductViewModel Product { get; set; }
        public List<int> ProductGroups { get; set; }
        public IActionResult OnGet(int? id)
        {
            if (id == null)
                return NotFound();
            
            var product = _productService.GetProduct(id.Value);
            Product = product;

            product.Categories = _productService.GetCategories();
            ProductGroups = _productService.GetProductGroups(id.Value);

            if (User == null)
                return NotFound();
            
            return Page();
        }
    }
}
