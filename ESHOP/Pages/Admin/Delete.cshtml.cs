using ESHOP.Models;
using ESHOP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ESHOP.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        private readonly IProductService _productService;
        public DeleteModel(IProductService productService) => _productService = productService;
        
        [BindProperty]
        public Product Product { get; set; }
        public void OnGet(int id) => Product = _productService.GetProductById(id);
        
        public IActionResult OnPost()
        {
            var product = _productService.GetProductById(Product.Id);
            var item = _productService.GetItemById(Product.Id);

            _productService.DeleteItem(item);
            _productService.DeleteProduct(product);
            _productService.Save();

            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                product.Id + "jpg");

            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
            
            return RedirectToPage("Index");
        }
    }
}
