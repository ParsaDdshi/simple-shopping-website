using ESHOP.Models;
using ESHOP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ESHOP.Pages.Admin.ManageGroups
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        public IndexModel(IProductService productService) => _productService = productService;
        
        public IList<Category> Category { get;set; }

        public void OnGet() => Category =_productService.GetCategories();
        
    }
}
