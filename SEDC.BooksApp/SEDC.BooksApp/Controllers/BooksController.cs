using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.BooksApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SEDC.BooksApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet] 
        public ActionResult<List<Book>> Get()
        {
            try
            {
                return Ok(StaticDb.Books);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpGet("queryString")] 
        public ActionResult<Book> GetByIndex(int index)
        {
            try
            {
                if (index < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad request, the index can not be negative!");
                }
                if (index >= StaticDb.Books.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Book with index {index} does not exist!");
                }
                return Ok(StaticDb.Books[index]);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpGet("filter")]  
        public ActionResult<List<Book>> FilterBooks(string author, string title)
        {
            try
            {
                if (string.IsNullOrEmpty(author) && string.IsNullOrEmpty(title))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "You have to send at least one filter parameter whether that would be author or title!");
                }
                if (string.IsNullOrEmpty(title))
                {
                    List<Book> booksDb = StaticDb.Books.Where(x => x.Author.ToLower().Contains(author.ToLower())).ToList();
                    return Ok(booksDb);
                }
                if (string.IsNullOrEmpty(author))
                {
                    List<Book> booksDb = StaticDb.Books.Where(x => x.Title.ToLower().Contains(title.ToLower())).ToList();
                    return Ok(booksDb);
                }
                List<Book> filteredBooks = StaticDb.Books.Where(x => x.Author.ToLower().Contains(author.ToLower())
                                                             && x.Title.ToLower().Contains(title.ToLower())).ToList();
                return Ok(filteredBooks);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpPost("postBook")] 
        public IActionResult PostBook([FromBody] Book book)
        {
            try
            {
                StaticDb.Books.Add(book);
                return StatusCode(StatusCodes.Status201Created, "Book was added");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpPost("booksTitles")]
        public ActionResult<List<string>> PostBooks([FromBody] List<Book> books)
        {
            try
            {
                List<string> bookTitles = books.Select(x => x.Title).ToList();
                return StatusCode(StatusCodes.Status200OK, bookTitles);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }
    }
}
