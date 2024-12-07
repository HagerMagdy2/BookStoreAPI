using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.DTOs.CustomerDTO
{
    public class EditCustomerDTO
    {
        [Required]
        public string id { get; set; }
        public string fullname { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string email { get; set; }
        public string Address { get; set; }
       
        [Required]
        public string phonenumber { get; set; }
    }
}
