using System.ComponentModel.DataAnnotations;

namespace ESHOP.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public bool IsOrderFinished { get; set; }

        public List<OrderDetail> Details { get; set; }
        public User User { get; set; }
    }
}