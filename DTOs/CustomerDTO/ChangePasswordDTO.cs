using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.DTOs.CustomerDTO
{
    public class ChangePasswordDTO
    {
        [Required]
        public string id { get; set; }
        [Required]
        public string old_password { get; set; }
        [Required]
        public string newPassword { get; set; }
        [Required]
        [Compare("newPassword", ErrorMessage ="Password not match")]
        public string Confirm_password { get; set; }

    }
}
