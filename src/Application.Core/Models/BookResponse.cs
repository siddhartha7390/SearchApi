using System;
using System.Collections.Generic;

namespace Application.Core.Models
{
    public class BookResponse
    { 
        public int BookId { get; set; }
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        public string Type { get; set; }
        public string Genre { get; set; }
        public List<AuthorResponse> Authors {get; set;}
    }
    public class AuthorResponse
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
    }
}