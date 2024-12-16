namespace BookStoreAPI.DTOs.OrderDTO
{
    public class EditOrderDTO
    {
        public List<OrderBookDTO> books { get; set; }
    }

    public class OrderBookDTO
    {
        public int book_id { get; set; }
        public int quantity { get; set; }
    }
}
