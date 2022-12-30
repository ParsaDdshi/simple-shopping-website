using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ESHOP.Services.Interfaces;
using ESHOP.Models;

namespace ESHOP.Pages.Admin.ManageUsers
{
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;
        public EditModel(IUserService userService) => _userService = userService;
        
        [BindProperty]
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            
            _userService.UpdateUser(User);
            try
            {
                _userService.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_userService.IsUserExistById(User.UserId))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToPage("./Index");
        }
    }
}
