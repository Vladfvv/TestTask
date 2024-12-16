using System.Text.Json.Serialization;

namespace TestTask.Models
{
    /// <summary>
    /// Book.
    /// DO NOT change anything here.
    /// </summary>
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int QuantityPublished { get; set; }
        public DateTime PublishDate { get; set; }
        public int AuthorId { get; set; }
        [JsonIgnore] //  для исключения свойств, которые вызывают циклические зависимости
        public virtual Author Author { get; set; }
    }
}
