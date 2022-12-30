using ESHOP.Models;
using ESHOP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ESHOP.Pages.Admin.ManageGroups
{
    public class EditModel : PageModel
    {
        private readonly IProductService _productService;
        public EditModel(IProductService productService) => _productService = productService;

        [BindProperty]
        public Category Category { get; set; }

        public  IActionResult OnGet(int? id)
        {
            if (id == null)
                return NotFound();
            
            Category = _productService.GetCategoryById(id.Value);

            if (Category == null)
                return NotFound();
            
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            
           _productService.UpdateCategory(Category);

            try
            {
               _productService.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_productService.IsCategoryExists(Category.Id))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToPage("./Index");
        }
    }
}
