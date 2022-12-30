using ESHOP.Models;
using ESHOP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ESHOP.Pages.Admin.ManageUsers
{
    public class CreateModel : PageModel
    {
        private readonly IUserService _userService;
        public CreateModel(IUserService userService) => _userService = userService;

        [BindProperty]
        public new User User { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            User.RegisterTime = DateTime.Now;
            _userService.AddUser(User);
            _userService.Save();

            return RedirectToPage("./Index");
        }
    }
}
