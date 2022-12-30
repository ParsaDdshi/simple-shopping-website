using ESHOP.Models;
using ESHOP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ESHOP.Pages.Admin.ManageGroups
{
    public class DetailsModel : PageModel
    {
        private readonly IProductService _productService;

        public DetailsModel(IProductService productService) => _productService = productService;

        public Category Category { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
                return NotFound();
            
            Category = _productService.GetCategoryById(id.Value);

            if (Category == null)
                return NotFound();
            
            return Page();
        }
    }
}
