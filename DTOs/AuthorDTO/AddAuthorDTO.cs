using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.DTOs.AuthorDTO
{
    public class AddAuthorDTO
    {
        
        public string name { get; set; }
        public string bio { get; set; }
        public int age { get; set; }
        public int NumberOfBooks { get; set; }
    }
}
