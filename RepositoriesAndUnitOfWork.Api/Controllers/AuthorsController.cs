using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RepositoriesAndUnitOfWork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IBaseRepository<Author> _authorRepo;

        public AuthorsController(IBaseRepository<Author> authorRepo)
        {
            _authorRepo = authorRepo;
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id)
        {
            var author = _authorRepo.GetById(id);
            return Ok(author);
        }
        [HttpGet("GetAuthorAsync")]
        public async Task<IActionResult> GetAuthorByIdAsync(int id)
        {
            var author = await _authorRepo.GetByIdAsync(id);
            return Ok(author);
        }

        
    }
}
