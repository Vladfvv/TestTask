using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }


        //3. Выводим авторов, написавших четное количество книг, изданных после 2015 года
        public async Task<List<Author>> GetAuthors()
        {
            var yearThreshold = new DateTime(2015, 1, 1);

            return await _context.Authors
                .Include(a => a.Books) // Загружаем книги для каждого автора
                .Where(a => a.Books.Any(b => b.PublishDate > yearThreshold)) // Убедимся, что у автора есть хотя бы одна книга после 2015 года
                .Where(a => a.Books.Count(b => b.PublishDate > yearThreshold) % 2 == 0) // Фильтруем по четному количеству книг
                .ToListAsync();
        }

        //4. Вывести автора, который написал книгу с самым длинным названием(в случае если таких авторов окажется несколько, необходимо вернуть автора с наименьшим Id)
        public async Task<Author> GetAuthor()
        {
            return await _context.Authors
                .Select(a => new
                {
                    Author = a,
                    LongestBook = a.Books
                        .OrderByDescending(b => b.Title.Length) // Сортируем книги по длине названия
                        .FirstOrDefault() // Выбираем книгу с самым длинным названием
                })
                .OrderByDescending(x => x.LongestBook != null ? x.LongestBook.Title.Length : 0) // Сортируем по длине названия самой длинной книги
                .ThenBy(x => x.Author.Id) // Затем по Id автора
                .Select(x => new Author
                {
                    Id = x.Author.Id,
                    Name = x.Author.Name,
                    Surname = x.Author.Surname,
                    Books = x.LongestBook != null ? new List<Book> { x.LongestBook } : new List<Book>() // Добавляем только самую длинную книгу
                })
                .FirstOrDefaultAsync();
        }
    }
}
