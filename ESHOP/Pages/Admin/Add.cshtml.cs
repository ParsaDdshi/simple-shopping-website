using ESHOP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ESHOP.Services.Interfaces;

namespace ESHOP.Pages.Admin
{
    public class AddModel : PageModel
    {
        private readonly IProductService _productService;

        public AddModel(IProductService productService) => _productService = productService;
        

        [BindProperty]
        public ProductViewModel ProductViewModel { get; set; }

        [BindProperty]
        public List<int> SelectedGroups { get; set; }
        public void OnGet()
        {
            ProductViewModel = new ProductViewModel()
            {
                Categories = _productService.GetCategories()
            };
        }

        public IActionResult OnPost()
        {
            ProductViewModel.Categories = _productService.GetCategories();
            if (!ModelState.IsValid)
                return Page();

            Item item = new Item()
            {
                Price = ProductViewModel.Price,
                QuantityInStock = ProductViewModel.QuantityInStock
            };

            Product product = new Product()
            {
                Name = ProductViewModel.Name,
                Description = ProductViewModel.Description,
                Item = item
            };


            if (ProductViewModel.Picture?.Length > 0)
            {
                _productService.InsertItem(item);
                _productService.Save();
                _productService.InsertProduct(product);
                _productService.Save();
                product.ItemId = product.Id;
                _productService.Save();
                string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images",
                    product.Id + ".jpg");
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    ProductViewModel.Picture.CopyTo(stream);
                }
            }
            else
            {
                ModelState.AddModelError("picture", "تصویر نمی تواند خالی باشد.");
                return Page();
            }

            if (SelectedGroups.Any() && SelectedGroups.Count > 0)
            {
                foreach(var gr in SelectedGroups)
                {
                    _productService.InsertCategoryToProduct(new CategoryToProduct
                    {
                        CategoryId = gr,
                        ProductId = product.Id
                    });
                }
                _productService.Save();
            }
            return RedirectToPage("Index");
        }
    }
}
