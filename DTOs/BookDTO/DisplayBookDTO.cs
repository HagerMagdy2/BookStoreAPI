using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.DTOs.BookDTO
{
    public class DisplayBookDTO
    {
        public int id { get; set; }
        
        public string title { get; set; }
        
        public decimal price { get; set; }
        public int srock { get; set; }
       
        public DateOnly publishdate { get; set; }
        public string authorname { get; set; }
        public string catalog { get; set; }
    }
}
