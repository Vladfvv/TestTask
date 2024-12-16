using TestTask.Models;

namespace TestTask.Services.Interfaces
{
    /// <summary>
    /// Authors service defenition.
    /// DO NOT change anything here. Use created contract and provide only needed implementation.
    /// </summary>
    public interface IAuthorService
    {
        public Task<Author> GetAuthor();
        public Task<List<Author>> GetAuthors();
    }
}
