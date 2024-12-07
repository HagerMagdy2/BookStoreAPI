using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.DTOs.CustomerDTO
{
    public class AddCustomerDTO
    {
        public string fullname { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        [RegularExpression("[a-z0-9]+@[a-z]+\\.[a-z]{2,3}", ErrorMessage = "invalid Email")]
        public string email { get; set; }
        public string Address { get; set; }
        [Required]
        [RegularExpression("^((?=\\S*?[A-Z])(?=\\S*?[a-z])(?=\\S*?[0-9]).{6,})\\S$", ErrorMessage = "invalid password")]
        public string password { get; set; }
        [Required]
        public string phonenumber { get; set; }
    }
}
