using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RepositoriesAndUnitOfWork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBaseRepository<Book> _bookRepo;

        public BooksController(IBaseRepository<Book> bookRepo)
        {
           _bookRepo = bookRepo;
        }

        [HttpGet]
        public IActionResult GetBookById(int id)
        {
            var book = _bookRepo.GetById(id);
            return Ok(book);    
        }

        [HttpGet("GetBooks")]
        public IActionResult GetBooks()
        {
            var books = _bookRepo.GetAll();
            return Ok(books);
        }

        [HttpGet("GetByName")]
        public IActionResult GetBookByName(string name)
        {
            var book = _bookRepo.Find(b => b.Title == name);
            return Ok(book);
        }
    }
}
