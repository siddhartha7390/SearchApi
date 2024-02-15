using System.Collections.Generic;
using System.Net;

namespace Application.Core.Models.Poco
{
    public class Book
    {
        public Book()
        {
            Authors = new HashSet<Author>();
        }
        public int BookId { get; set; }
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        public string Type { get; set; }
        public string Genre { get; set; }
        public int AuthorId { get; set; }
        public ICollection<Author> Authors {get; set;}
    }
}