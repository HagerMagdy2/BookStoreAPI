using BookStoreAPI.DTOs.AuthorDTO;
using BookStoreAPI.Models;
using BookStoreAPI.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin,customer")]

    public class AuthorController : ControllerBase
    {
        UnitOFWork _unit;
        public AuthorController(UnitOFWork _unit)
        {
            this._unit = _unit;
        }
        [HttpGet]
        [SwaggerOperation(Summary = "Get all Authors")]
        [SwaggerResponse(200, "Return all Authors", typeof(List<DisplayAuthorDTO>))]
        public IActionResult getAll()
        {
            List<Author> authors = _unit.AuthorsRepositry.selectall();
            List<DisplayAuthorDTO> authorsDTO = new List<DisplayAuthorDTO>();
            foreach (Author author in authors)
            {
                DisplayAuthorDTO authorDTO = new DisplayAuthorDTO()
                {
                   id=author.id,
                   name=author.name,
                   bio=author.bio,
                   age=author.age,
                   NumberOfBooks=author.NumberOfBooks,
                };
                authorsDTO.Add(authorDTO);
            }
            return Ok(authorsDTO);
        }
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Select Author by Id")]
        [SwaggerResponse(200, "Return Author by Id", typeof(DisplayAuthorDTO))]
        [SwaggerResponse(404, "Return not Found")]
        public IActionResult getById(int id)
        {
            Author author = _unit.AuthorsRepositry.selectbyid(id);

            if (author == null)
            {
                return NotFound();
            }
            else
            {
                DisplayAuthorDTO AuthorDTO = new DisplayAuthorDTO()
                {
                    id = author.id,
                    name = author.name,
                    bio = author.bio,
                    age = author.age,
                    NumberOfBooks = author.NumberOfBooks
                   
                };
                return Ok(AuthorDTO);
            }
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [SwaggerOperation(Summary = "Add Author")]
        [SwaggerResponse(201, "Return Created if Author Add Successfully")]
        [SwaggerResponse(400, "If Invalid Author Data")]
        public IActionResult add(AddAuthorDTO author)
        {
            if (ModelState.IsValid)
            {
                Author  author1 = new Author()
                {
                    name = author.name,
                    bio = author.bio,
                    age = author.age,
                    NumberOfBooks = author.NumberOfBooks,
                };
                _unit.AuthorsRepositry.add(author1);
                _unit.savechanges();
                return CreatedAtAction("getById", new { id = author1.id }, author);
            }
            return BadRequest(ModelState);
        }
        [HttpPut]
        [Authorize(Roles = "admin")]
        [SwaggerOperation(Summary = "Edit in a Author data")]
        [SwaggerResponse(204, "if Author updated Successfully")]
        [SwaggerResponse(400, "If Invalid Author Data")]
        public IActionResult edit(int id,AddAuthorDTO authorDTO)
        {
            if (ModelState.IsValid)
            {
                Author author = new Author()
                {
                    id=id,
                    name = authorDTO.name,
                    bio = authorDTO.bio,
                    age = authorDTO.age,
                    NumberOfBooks = authorDTO.NumberOfBooks,

                };
                _unit.AuthorsRepositry.update(author);
                _unit.savechanges();
                return NoContent();

            }
            return BadRequest(ModelState);
        }
        [HttpDelete]
        [Authorize(Roles = "admin")]
        [SwaggerOperation(Summary = "Delete a Author")]
        [SwaggerResponse(200, " if Author Deleted Successfully")]
        public IActionResult delete(int id)
        {
            _unit.AuthorsRepositry.delete(id);
            _unit.savechanges();
            return Ok();
        }

    }
}
