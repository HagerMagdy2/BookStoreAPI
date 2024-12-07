using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models
{
    public class Catlog
    {
        public int id { get; set; }
        [StringLength(50)]
        [Required]
        public string name { get; set; }
        public string description { get; set; }
        public virtual List<Book> Books { get; set; } = new List<Book>();
    }
}
