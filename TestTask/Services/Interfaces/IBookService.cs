using TestTask.Models;

namespace TestTask.Services.Interfaces
{
    /// <summary>
    /// Books service defenition.
    /// DO NOT change anything here. Use created contract and provide only needed implementation.
    /// </summary>
    public interface IBookService
    {
        public Task<Book> GetBook();
        public Task<List<Book>> GetBooks();
    }
}
