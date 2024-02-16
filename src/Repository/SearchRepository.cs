using System.Collections.Generic;
using System.Linq;
using Application.Core.Interfaces.Repository;
using Application.Core.Models;
using Application.Core.Models.Poco;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class SearchRepository : ISearchRepository
    {
        private readonly ApplicationDbContext _context;

        public SearchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Book> Search(SearchRequest request)
        {
            var bookResults = _context.Books.Include(x => x.Authors)
                .Where(c => c.Title.ToLower().Contains(request.Title) ||
                            c.Type.ToLower().Contains(request.Type) ||
                            c.Genre.ToLower().Contains(request.Genre) ||
                            c.PublicationYear == request.PublicationYear ||
                            c.Authors.Any(p => p.FirstName.ToLower().Contains(request.AuthorName) ||
                                            p.LastName.ToLower().Contains(request.AuthorName)))
                .ToList();
            return bookResults;
        }
    }
}