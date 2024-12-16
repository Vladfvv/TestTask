using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        // 2.Выводим книгу с наибольшей стоимостью опубликованного тиража       
        public async Task<Book> GetBook()
        {
            return await _context.Books
                .OrderByDescending(b => b.Price * b.QuantityPublished)//сортировка книг по убыванию стоимости опубликованного тиража 
                .FirstOrDefaultAsync();
        }

        //1. Выводим книги, в названии которой содержится "Red" и которые опубликованы после выхода альбома "Carolus Rex" группы Sabaton
        public async Task<List<Book>> GetBooks()
        {
            // Получаем дату выхода альбома "Carolus Rex" из Program
            DateTime carolusRexReleaseDate = await Program.GetCarolusRexReleaseDateAsync();

            return await _context.Books
                .Where(b => EF.Functions.Like(b.Title, "%Red%") && // Используем SQL-функцию LIKE для поиска без учета регистра
                           b.PublishDate > carolusRexReleaseDate) //сравнение дат
                .ToListAsync();
        }
    }
}
