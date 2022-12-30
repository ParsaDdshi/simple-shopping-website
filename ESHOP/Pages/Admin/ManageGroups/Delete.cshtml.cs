using ESHOP.Models;
using ESHOP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ESHOP.Pages.Admin.ManageGroups
{
    public class DeleteModel : PageModel
    {
        private readonly IProductService _productService;

        public DeleteModel(IProductService productService) => _productService = productService;

        [BindProperty]
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

        public IActionResult OnPost(int? id)
        {
            if (id == null)
                return NotFound();
            

            Category = _productService.GetCategoryById(id.Value);

            if (Category != null)
            {
                _productService.RemoveCategoryRelations(id.Value);
                _productService.RemoveCategory(Category);
                _productService.Save();
            }

            return RedirectToPage("./Index");
        }
    }
}
