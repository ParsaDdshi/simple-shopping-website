using System.ComponentModel;

namespace ESHOP.Models
{
    public class Category
    {
        public int Id { get; set; }
        [DisplayName("نام گروه")]
        public string Name { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }

        public ICollection<CategoryToProduct>? CategoryToProducts { get; set; }
    }
}