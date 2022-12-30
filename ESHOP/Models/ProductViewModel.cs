using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ESHOP.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [DisplayName("نام")]
        [Required]
        public string Name { get; set; }
        [DisplayName("قیمت")]
        [Required]
        public decimal Price { get; set; }
        [DisplayName("توضیحات")]
        [Required]
        public string Description { get; set; }
        [DisplayName("تعداد کالا در انبار")]
        [Required]
        public int QuantityInStock { get; set; }
        [DisplayName("تصویر")]
        public IFormFile? Picture { get; set; }
        [DisplayName("گروه های محصول")]
        public List<Category>? Categories { get; set; }
    }
}