using Microsoft.AspNetCore.Mvc;
using TestTask.Services.Interfaces;

namespace TestTask.Controllers
{
    /// <summary>
    /// Authors controller.
    /// DO NOT change anything here. Use created contract and provide only needed implementation.
    /// </summary>
    [Route("api/v1/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        /// <summary>
        /// Returns selected author. 
        /// Selection rules are specified in Task description provided by recruiter
        /// </summary>
        [HttpGet]
        [Route("selected-author")]
        public async Task<IActionResult> Get()
        {
            var result = await _authorService.GetAuthor();
            return Ok(result);
        }

        /// <summary>
        /// Returns list of selected authors. 
        /// Selection rules are specified in Task description provided by recruiter
        /// </summary>
        [HttpGet]
        [Route("selected-authors")]
        public async Task<IActionResult> GetAuthors()
        {
            var result = await _authorService.GetAuthors();
            return Ok(result);
        }
    }
}
