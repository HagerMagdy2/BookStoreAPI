using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.DTOs.AuthorDTO
{
    public class DisplayAuthorDTO
    {
        public int id { get; set; }
        [StringLength(50)]
        [Required]
        public string name { get; set; }
        public string bio { get; set; }
        public int age { get; set; }
        public int NumberOfBooks { get; set; }
    }
}
