using ESHOP.Models;
using ESHOP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ESHOP.Pages.Admin.ManageUsers
{
    public class DetailsModel : PageModel
    {
        private readonly IUserService _userService;
        public DetailsModel(IUserService userService) => _userService = userService;
        

        public new User User { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
                return NotFound();
            
            User = _userService.GetUserById(id.Value);

            if (User == null)
                return NotFound();
            
            return Page();
        }
    }
}
