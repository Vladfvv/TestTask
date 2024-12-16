using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Services.Implementations;
using TestTask.Services.Interfaces;

namespace TestTask
{
    public class Program
    {

        // в ASP.NET Core метод Main должен быть асинхронным, чтобы использовать await в содержащихся в нем методах
        public static async Task Main(string[] args) // Сделали метод Main асинхронным
        {
            var builder = WebApplication.CreateBuilder(args);

            //DI: Регистрация сервисов билдером  как Scope, т.е. сервисов, которые должны существовать в рамках одного запроса и могут хранить состояние, специфичное для этого запроса
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            // Запуск приложения
            await app.RunAsync(); // Используем асинхронный запуск

            // Получаем дату выхода альбома "Carolus Rex"
            DateTime carolusRexReleaseDate = await GetCarolusRexReleaseDateAsync();

            // Вывод даты в консоль 
            Console.WriteLine($"Дата выхода альбома 'Carolus Rex': {carolusRexReleaseDate}");
            
        }

        // получить дату выхода альбома "Carolus Rex" группы Sabaton с Википедии.

        public static async Task<DateTime> GetCarolusRexReleaseDateAsync()
        {
            try
            {
                // URL страницы Википедии с информацией об альбоме "Carolus Rex"
                string url = "https://en.wikipedia.org/wiki/Carolus_Rex";

                // Выполняем HTTP-запрос
                using HttpClient client = new HttpClient();
                string htmlContent = await client.GetStringAsync(url);

                // Парсим HTML с помощью HtmlAgilityPack
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlContent);

                // Ищем дату выхода альбома
                string releaseDate = FindReleaseDate(doc);

                if (!string.IsNullOrEmpty(releaseDate))
                {
                    // Преобразуем строку в DateTime
                    if (DateTime.TryParse(releaseDate, out DateTime parsedDate))
                    {
                        return parsedDate;
                    }
                }

                // Если дата не найдена, возвращаем дефолтную дату (25 мая 2012 года)
                return new DateTime(2012, 5, 25);
            }
            catch (Exception ex)
            {
                // Логируем ошибку (например, в консоль или файл)
                Console.WriteLine($"Ошибка при получении даты выхода альбома: {ex.Message}");

                // Возвращаем дефолтную дату в случае ошибки
                return new DateTime(2012, 5, 25);
            }
        }

       
        /// Ищем дату выхода альбома в HTML-документе.
        
        private static string FindReleaseDate(HtmlDocument doc)
        {
            try
            {
                // Ищем элемент, содержащий дату выхода альбома. На Википедии дата обычно находится в таблице с классом "infobox"
                var infobox = doc.DocumentNode.SelectSingleNode("//table[contains(@class, 'infobox')]");

                if (infobox != null)
                {
                    // Ищем строку с заголовком "Released"
                    var releaseRow = infobox.SelectSingleNode(".//th[text()='Released']/following-sibling::td");

                    if (releaseRow != null)
                    {
                        return releaseRow.InnerText.Trim();
                    }
                }

                return null;
            }
            catch (Exception ex)
            {                
                Console.WriteLine($"Ошибка при парсинге даты выхода альбома: {ex.Message}"); // Логируем ошибку
                return null;
            }
        }
    }
}
