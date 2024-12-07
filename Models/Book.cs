using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPI.Models
{
    public class Book
    {
        public int id { get; set; }
        [StringLength(150)]
        public string title { get; set; }
        [Column(TypeName ="money")]
        public decimal price { get; set; }
        public int srock { get; set; }
        [Column(TypeName = "date")]
        public DateOnly publishdate { get; set; }
        [ForeignKey("catlog")]
        public int cat_id { get; set; }
        public virtual Catlog? catlog { get; set; }
        [ForeignKey("author")]
        public int author_id { get; set; }
        public virtual Author? author { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
}
