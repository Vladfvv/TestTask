using Microsoft.AspNetCore.Mvc;
using TestTask.Services.Interfaces;

namespace TestTask.Controllers
{
    /// <summary>
    /// Books controller.
    /// DO NOT change anything here. Use created contract and provide only needed implementation.
    /// </summary>
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Returns selected book. 
        /// Selection rules are specified in Task description provided by recruiter
        /// </summary>
        [HttpGet]
        [Route("selected-book")]
        public async Task<IActionResult> Get()
        {
            var result = await _bookService.GetBook();
            return Ok(result);
        }

        /// <summary>
        /// Returns list of selected books. 
        /// Selection rules are specified in Task description provided by recruiter
        /// </summary>
        [HttpGet]
        [Route("selected-books")]
        public async Task<IActionResult> GetBooks()
        {
            var result = await _bookService.GetBooks();
            return Ok(result);
        }
    }
}
