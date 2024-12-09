using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.DTOs.BookDTO
{
    public class AddBookDTO
    {
        [Required]
        public string title { get; set; }
        [Range(20, 1000,ErrorMessage ="Invalid Price, Price Must between 20 to 1000")]
        public decimal price { get; set; }
        [Range(0, 500, ErrorMessage = "Invalid Stock number")]

        public int srock { get; set; }

        public DateOnly publishdate { get; set; }

        public int cat_id { get; set; }

        public int author_id { get; set; }

    }
}
