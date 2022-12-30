using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ESHOP.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(300)]
        [DisplayName("نام کامل")]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("شماره تلفن")]
        [MaxLength(100)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [MaxLength(300)]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        [Remote("VerifyEmail", "Account")]
        public string Email { get; set; }
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "تکرار کلمه عبور")]
        public string RePassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [MaxLength(300)]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}