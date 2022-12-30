using ESHOP.Models;
using ESHOP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ESHOP.Pages.Admin.ManageGroups
{
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;

        public CreateModel(IProductService productService) => _productService = productService;

        public IActionResult OnGet() => Page();
        

        [BindProperty]
        public Category Category { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            
            _productService.InsertCategory(Category);
            _productService.Save();

            return RedirectToPage("./Index");
        }
    }
}
