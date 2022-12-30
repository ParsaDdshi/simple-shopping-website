using ESHOP.Models;
using ESHOP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ESHOP.Pages.Admin.ManageUsers
{
    public class DeleteModel : PageModel
    {
        private readonly IUserService _userService;
        public DeleteModel(IUserService userService) => _userService = userService;

        [BindProperty]
        public User User { get; set; }


        public IActionResult OnGet(int? id)
        {
            if(id == null)
                return NotFound();

            User = _userService.GetUserById(id.Value);

            if(User == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
                return NotFound();

            User = _userService.GetUserById(id.Value);

            if (User == null)
                return NotFound();

            _userService.RemoveUser(User);
            _userService.Save();

            return RedirectToPage("./Index");
        }
    }
}
