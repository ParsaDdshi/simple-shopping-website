using System.ComponentModel;

namespace ESHOP.Models
{
    public class Product
    {
        public int Id { get; set; }
        [DisplayName("نام")]
        public string Name { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }
        public int ItemId { get; set; }

        public ICollection<CategoryToProduct> CategoryToProducts { get; set; }
        public Item Item { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
    }
}