namespace Application.Core.Models
{
    public class SearchRequest
    { 
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        public string Type { get; set; }
        public string Genre { get; set; }
        public string AuthorName { get; set; }
    }
}