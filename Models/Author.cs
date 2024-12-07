using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models
{
    public class Author
    {
        public int id { get; set; }
        [StringLength(50)]
        [Required]
        public string name { get; set; }
        public string bio { get; set; }
        public int age { get; set; }
        public int NumberOfBooks { get; set; }
        public virtual List<Book> Books { get; set; }=new List<Book>();

    }
}
