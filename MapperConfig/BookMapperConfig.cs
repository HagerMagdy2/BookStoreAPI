using AutoMapper;
using BookStoreAPI.DTOs.BookDTO;
using BookStoreAPI.Models;

namespace BookStoreAPI.MapperConfig
{
    public class BookMapperConfig:Profile
    {
        public BookMapperConfig()
        {
            CreateMap<Book,AddBookDTO>().ReverseMap();
        }
    }
}
