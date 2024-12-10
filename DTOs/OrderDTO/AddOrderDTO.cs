using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPI.DTOs.OrderDTO
{
    public class AddOrderDTO
    {
        
       
        public string cust_id { get; set; }
       public List<AddOrderDetailsDTO> books { get; set; } =new List<AddOrderDetailsDTO>();
    }
}
