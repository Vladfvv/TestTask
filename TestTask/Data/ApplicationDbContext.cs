using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask.Data
{
    /// <summary>
    /// ApplicationDbContext.
    /// DO NOT change anything here.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected ApplicationDbContext(DbContextOptions options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                [
                    new Author {Id = 1, Name = "John", Surname = "Smith"},
                    new Author {Id = 2, Name = "Ivan", Surname = "Karpov"},
                    new Author {Id = 3, Name = "Pavel", Surname = "Doe"},
                    new Author {Id = 6, Name = "Frank", Surname = "Sidorov"}
                    ]
                );

            modelBuilder.Entity<Book>().HasData(
                [
                    new Book { Id=1, AuthorId = 1, Title = "The Red Army", Price = 15.03, QuantityPublished = 1600, PublishDate = new DateTime(2015, 8, 15, 12, 0, 0)},
                    new Book { Id=3, AuthorId = 2, Title = "Something forbidden", Price = 4, QuantityPublished = 10, PublishDate = new DateTime(2020, 7, 13, 12, 0, 0)},
                    new Book { Id=4, AuthorId = 2, Title = "Well", Price = 6, QuantityPublished = 100, PublishDate = new DateTime(2016, 7, 24, 12, 0, 0)},
                    new Book { Id=5, AuthorId = 3, Title = "Bird in a cage", Price = 44, QuantityPublished = 16203, PublishDate = new DateTime(2016, 2, 22, 12, 0, 0)},
                    new Book { Id=7, AuthorId = 3, Title = "Need for speed", Price = 14.03, QuantityPublished = 1640, PublishDate = new DateTime(2016, 1, 28, 12, 0, 0)},
                    new Book { Id=10, AuthorId = 6, Title = "Even coolest story", Price = 1521.03, QuantityPublished = 11600, PublishDate = new DateTime(2003, 4, 28, 12, 0, 0)}
                ]
                    );
        }
    }
}
