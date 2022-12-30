using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ESHOP.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(300)]
        [DisplayName("نام کامل")]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("شماره تلفن")]
        [MaxLength(100)]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(300)]
        [DisplayName("ایمیل")]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("رمزعبور")]
        public string Password { get; set; }
        [Required]
        [DisplayName("تاریخ ثبت نام")]
        [DataType(DataType.DateTime)]
        public DateTime RegisterTime { get; set; }
        [DisplayName("ادمین")]
        public bool IsAdmin { get; set; }


        public List<Order>? Orders { get; set; }
    }
}