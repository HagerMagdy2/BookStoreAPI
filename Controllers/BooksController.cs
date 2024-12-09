using BookStoreAPI.DTOs.BookDTO;
using BookStoreAPI.Models;
using BookStoreAPI.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin,customer")]
    public class BooksController : ControllerBase
    {
        UnitOFWork _unit;
        public BooksController(UnitOFWork _unit)
        {
            this._unit = _unit;

        }
        [HttpGet]
        public IActionResult getAll()
        {
            List<Book> books = _unit.BooksRepositry.selectall();
            List<DisplayBookDTO> booksDTO = new List<DisplayBookDTO>();
            foreach (Book book in books)
            {
                DisplayBookDTO bookDTO = new DisplayBookDTO()
                {
                    id = book.id,
                    title = book.title,
                    price = book.price,
                    publishdate = book.publishdate,
                    srock = book.srock,
                    catalog = book.catlog.name,
                    authorname = book.author.name
                };
                booksDTO.Add(bookDTO);
            }
            return Ok(booksDTO);
        }

        [HttpGet("{id}")]
        public IActionResult getById(int id)
        {
            Book book = _unit.BooksRepositry.selectbyid(id);

            if (book == null)
            {
                return NotFound();
            }
            else
            {
                DisplayBookDTO bookDTO = new DisplayBookDTO()
                {
                    id = book.id,
                    title = book.title,
                    price = book.price,
                    publishdate = book.publishdate,
                    srock = book.srock,
                    catalog = book.catlog.name,
                    authorname = book.author.name
                };
                return Ok(bookDTO);
            }
        }
        [HttpPost]
        [Authorize(Roles ="admin")]
        public IActionResult add(AddBookDTO bookDTO)
        {
            if (ModelState.IsValid)
            {
                Book book = new Book()
                {
                    title = bookDTO.title,
                    price = bookDTO.price,
                    publishdate = bookDTO.publishdate,
                    srock = bookDTO.srock,
                    author_id = bookDTO.author_id,
                    cat_id = bookDTO.cat_id,
                };
                _unit.BooksRepositry.add(book);
                _unit.savechanges();
                return CreatedAtAction("getById", new { id = book.id }, bookDTO);
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{id}")]
        [Authorize(Roles ="admin")]
        public IActionResult edit(int id, AddBookDTO bookDTO)
        {
            if (ModelState.IsValid)
            {
                //_unit.BooksRepositry.update
                //fetch by id and change way
                //Book book = _unit.BooksRepositry.selectbyid(id);
                //if (book == null)
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    book.title = bookDTO.title;
                //    book.price = bookDTO.price;
                //    book.publishdate = bookDTO.publishdate;
                //    book.srock = bookDTO.srock;
                //    book.author_id = bookDTO.author_id;
                //    book.cat_id = bookDTO.cat_id;
                //}
                Book book = new Book()
                {
                    id = id,
                    title = bookDTO.title,
                    price = bookDTO.price,
                    publishdate = bookDTO.publishdate,
                    srock = bookDTO.srock,
                    author_id = bookDTO.author_id,
                    cat_id = bookDTO.cat_id,
                };

                _unit.BooksRepositry.update(book);
                _unit.savechanges();
                return NoContent();
            }
            return BadRequest(ModelState);
        }
        [HttpDelete]
        [Authorize(Roles ="admin")]
        public IActionResult delete(int id)
        {
            if (ModelState.IsValid)
            {
                _unit.BooksRepositry.delete(id);
                _unit.savechanges();
                return Ok();
            }
            return BadRequest(ModelState);
        }
    }
}
