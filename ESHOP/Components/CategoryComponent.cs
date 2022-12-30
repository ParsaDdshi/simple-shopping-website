using ESHOP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Components
{
    public class CategoryComponent : ViewComponent
    {
        private IProductService _productService;
        public CategoryComponent(IProductService productService) => _productService = productService;
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = _productService.GetCategoryForSidebar();
            return View("/Views/Components/CategoryComponent.cshtml",categories);
        }
    }
}