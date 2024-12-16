using System.Text.Json.Serialization;

namespace TestTask.Models
{
    /// <summary>
    /// Author.
    /// DO NOT change anything here.
    /// </summary>
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }        
        public virtual IList<Book> Books { get; set; }
    }
}
