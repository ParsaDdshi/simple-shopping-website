using ESHOP.Models;
using ESHOP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ESHOP.Pages.Admin.ManageUsers
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        public IndexModel(IUserService userService) => _userService = userService;
        
        public new IList<User> User { get;set; }

        public void OnGet() => User = _userService.GetAllUsers();
        
    }
}
