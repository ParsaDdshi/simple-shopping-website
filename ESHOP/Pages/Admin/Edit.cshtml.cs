using ESHOP.Models;
using ESHOP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ESHOP.Pages.Admin
{
    public class EditModel : PageModel
    {
        private readonly IProductService _productService;
        public EditModel(IProductService productService) => _productService = productService;
        
        [BindProperty]
        public ProductViewModel Product { get; set; }
        [BindProperty]
        public List<int> SelectedGroups { get; set; }
        [BindProperty]
        public List<int> ProductGroups { get; set; }

        public void OnGet(int id)
        {
            var product = _productService.GetProduct(id);
            Product = product;

            product.Categories = _productService.GetCategories();
            ProductGroups = _productService.GetProductGroups(id);
        }

        public IActionResult OnPost()
        {
            Product.Categories = _productService.GetCategories();
            if (!ModelState.IsValid)
                return Page();

            var product = _productService.GetProductById(Product.Id);
            var item = _productService.GetItemById(Product.Id);

            product.Name = Product.Name;
            product.Description = Product.Description;
            item.Price = Product.Price;
            item.QuantityInStock = Product.QuantityInStock;

            _productService.Save();

            if (Product.Picture?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images",
                    Product.Id + ".jpg");
                System.IO.File.Delete(filePath);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    Product.Picture.CopyTo(stream);
                };
            }

            _productService.DeleteCategoryToProducts(Product.Id);
            _productService.EditCategoryToProducts(SelectedGroups, Product.Id);

            return RedirectToPage("Index");
        }
    }
}
