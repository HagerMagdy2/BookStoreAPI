using BookStoreAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPI.DTOs.OrderDTO
{
    public class AddOrderDetailsDTO
    {
        public virtual int book_id { get; set; }
        public int quantity { get; set; }
    }
}
