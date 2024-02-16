using Application.Core.Interfaces;
using Application.Core.Interfaces.Repository;
using Application.Core.Models.Poco;
using Application.Core.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Application.Core.Test;

public class SearchServiceTest
{
    private readonly ISearchRepository _searchRepository;
    private readonly ISearchService _searchService;

    public SearchServiceTest()
    {
        _searchRepository = Substitute.For<ISearchRepository>();
        var logger = Substitute.For<ILogger<SearchService>>();
        _searchService = new SearchService(_searchRepository, logger);
    }

    [Fact]
    public void SearchService_should_return_books_and_authors()
    {
        _searchRepository.Search(new Models.SearchRequest()).ReturnsForAnyArgs(GetBooks());
        var response = _searchService.GetData(new Models.SearchRequest{ Title = "ABC", Type = "Sci-fi"});
        response.Should().NotBeNullOrEmpty();
        Assert.Single(response);
    }

    [Fact]
    public void SearchService_should_not_return_books_without_titles()
    {
        var books = GetBooks();
        books.Add(new Book { BookId = 2 });
        _searchRepository.Search(new Models.SearchRequest()).ReturnsForAnyArgs(GetBooks());
        var response = _searchService.GetData(new Models.SearchRequest{ Title = "ABC", Type = "Sci-fi"});
        response.Should().NotBeNullOrEmpty();
        Assert.Single(response);
        Assert.Equal(1, response.First().BookId);
    }

    [Fact]
    public void SearchService_should_not_return_books_without_Authors_firstorlast_name()
    {
        _searchRepository.Search(new Models.SearchRequest()).ReturnsForAnyArgs(GetBooks());
        var response = _searchService.GetData(new Models.SearchRequest{ Title = "ABC", Type = "Sci-fi"});
        response.Should().NotBeNullOrEmpty();
        Assert.Single(response);
        Assert.Equal("Sid B", response.First().Authors.First().Name);
    }

    private List<Book> GetBooks()
    {
        return new List<Book>
        {
            new Book
            {
                BookId = 1,
                Title = "ABC",
                Type = "Sci-fi",
                PublicationYear = 1990,
                Authors = new List<Author>
                {
                    new Author 
                    {
                        AuthorId = 1,
                        FirstName = "Sid",
                        LastName = "B",
                        DateOfBirth = new DateTime(1989,8,9)
                    },
                    new Author 
                    {
                        AuthorId = 2,
                        FirstName = "Sid",
                        DateOfBirth = new DateTime(1989,8,9)
                    }
                }
            }
        };
    }
}