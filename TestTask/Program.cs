using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Services.Implementations;
using TestTask.Services.Interfaces;

namespace TestTask
{
    public class Program
    {

        // � ASP.NET Core ����� Main ������ ���� �����������, ����� ������������ await � ������������ � ��� �������
        public static async Task Main(string[] args) // ������� ����� Main �����������
        {
            var builder = WebApplication.CreateBuilder(args);

            //DI: ����������� �������� ��������  ��� Scope, �.�. ��������, ������� ������ ������������ � ������ ������ ������� � ����� ������� ���������, ����������� ��� ����� �������
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

            // ������ ����������
            await app.RunAsync(); // ���������� ����������� ������

            // �������� ���� ������ ������� "Carolus Rex"
            DateTime carolusRexReleaseDate = await GetCarolusRexReleaseDateAsync();

            // ����� ���� � ������� 
            Console.WriteLine($"���� ������ ������� 'Carolus Rex': {carolusRexReleaseDate}");
            
        }

        // �������� ���� ������ ������� "Carolus Rex" ������ Sabaton � ���������.

        public static async Task<DateTime> GetCarolusRexReleaseDateAsync()
        {
            try
            {
                // URL �������� ��������� � ����������� �� ������� "Carolus Rex"
                string url = "https://en.wikipedia.org/wiki/Carolus_Rex";

                // ��������� HTTP-������
                using HttpClient client = new HttpClient();
                string htmlContent = await client.GetStringAsync(url);

                // ������ HTML � ������� HtmlAgilityPack
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlContent);

                // ���� ���� ������ �������
                string releaseDate = FindReleaseDate(doc);

                if (!string.IsNullOrEmpty(releaseDate))
                {
                    // ����������� ������ � DateTime
                    if (DateTime.TryParse(releaseDate, out DateTime parsedDate))
                    {
                        return parsedDate;
                    }
                }

                // ���� ���� �� �������, ���������� ��������� ���� (25 ��� 2012 ����)
                return new DateTime(2012, 5, 25);
            }
            catch (Exception ex)
            {
                // �������� ������ (��������, � ������� ��� ����)
                Console.WriteLine($"������ ��� ��������� ���� ������ �������: {ex.Message}");

                // ���������� ��������� ���� � ������ ������
                return new DateTime(2012, 5, 25);
            }
        }

       
        /// ���� ���� ������ ������� � HTML-���������.
        
        private static string FindReleaseDate(HtmlDocument doc)
        {
            try
            {
                // ���� �������, ���������� ���� ������ �������. �� ��������� ���� ������ ��������� � ������� � ������� "infobox"
                var infobox = doc.DocumentNode.SelectSingleNode("//table[contains(@class, 'infobox')]");

                if (infobox != null)
                {
                    // ���� ������ � ���������� "Released"
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
                Console.WriteLine($"������ ��� �������� ���� ������ �������: {ex.Message}"); // �������� ������
                return null;
            }
        }
    }
}
