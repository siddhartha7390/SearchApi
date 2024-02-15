using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                .Where(c => c.Title.Contains(request.Title) ||
                            c.Type.Contains(request.Type) ||
                            c.PublicationYear == request.PublicationYear ||
                            c.Authors.Any(p => p.FirstName.Contains(request.AuthorName) ||
                                            p.LastName.Contains(request.AuthorName)))
                .ToList();
            return bookResults;
        }
    }
}