using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPI.Models
{
    public class OrderDetails
    {
      
        [ForeignKey("order")]
        public virtual int order_id { get; set; }
        public virtual Order order { get; set; }
        [ForeignKey("book")]
        public virtual int book_id { get; set; }
        public virtual Book book { get; set; }
        public int quantity { get; set; }
        [Column(TypeName = "money")]

        public decimal unitprice { get; set; }
    }
}
