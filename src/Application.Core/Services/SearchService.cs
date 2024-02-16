using Application.Core.Models.Poco;
using System.Collections.Generic;
using Application.Core.Interfaces;
using Application.Core.Interfaces.Repository;
using Application.Core.Models;
using System.Linq;
using System;
using Microsoft.Extensions.Logging;
namespace Application.Core.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;
        private ILogger<SearchService> _logger;
        public SearchService(ISearchRepository searchRepository, ILogger<SearchService> logger)
        {
            _searchRepository = searchRepository;
            _logger = logger;
        }

        public List<BookResponse> GetData(SearchRequest request)
        {
            return MapToResponse(_searchRepository.Search(request));
        }

        private List<BookResponse> MapToResponse(List<Book> books)
        {
            var response = new List<BookResponse>();
            if(books.Any())
            {
                foreach(var item in books)
                {
                    if(string.IsNullOrEmpty(item.Title))
                    {
                        _logger.LogWarning($"Title is missing for the Book Id {item.BookId}");
                        continue;
                    }
                    var authors = new List<AuthorResponse>();
                    foreach (var author in item.Authors)
                    {
                        if(string.IsNullOrEmpty(author.FirstName) || string.IsNullOrEmpty(author.LastName))
                            _logger.LogWarning($"FirstName or LastName is missing for AuthorId {author.AuthorId}");
                        else
                            authors.Add(new AuthorResponse
                            {
                                AuthorId = author.AuthorId,
                                Name = $"{author.FirstName} {author.LastName}",
                                Age = CalculateAge(author.DateOfBirth),
                                Gender = author.Gender
                            });
                    }
                    response.Add(new BookResponse {
                        BookId = item.BookId,
                        Title = item.Title,
                        Genre = item.Genre,
                        PublicationYear = item.PublicationYear,
                        Type = item.Type,
                        Authors = authors
                    });
                }
            }
            return response;
        }
        private int CalculateAge(DateTime dateOfBirth)
        {
            DateTime currentDate = DateTime.Today;

            int age = currentDate.Year - dateOfBirth.Year;

            if (currentDate.Month < dateOfBirth.Month || (currentDate.Month == dateOfBirth.Month && currentDate.Day < dateOfBirth.Day))
            {
                age--;
            }
            return age;
        }
    }
}