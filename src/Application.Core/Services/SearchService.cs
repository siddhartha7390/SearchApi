using Application.Core.Models.Poco;
using System.Collections.Generic;
using Application.Core.Interfaces;
using Application.Core.Interfaces.Repository;
using System.Threading.Tasks;
using Application.Core.Models;
using System.Linq;
using System;
namespace Application.Core.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;
        public SearchService(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
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
                    response.Add(new BookResponse {
                        BookId = item.BookId,
                        Title = item.Title,
                        Genre = item.Genre,
                        PublicationYear = item.PublicationYear,
                        Type = item.Type,
                        Authors = item.Authors.Select( x =>
                            new AuthorResponse()
                            {
                                AuthorId = x.AuthorId,
                                Name = $"{x.FirstName} {x.LastName}",
                                Age = CalculateAge(x.DateOfBirth),
                                Gender = x.Gender
                            }
                        ).ToList()
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