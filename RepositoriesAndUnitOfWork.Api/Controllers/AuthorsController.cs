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

        [HttpGet]
        public IActionResult GetAuthorById(int id)
        {
            var author = _authorRepo.GetById(id);
            return Ok(author);
        }
    }
}
