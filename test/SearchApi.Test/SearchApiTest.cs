using Application.Core.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Application.Core.Models;

namespace SearchApi.Test;

public class SearchApiTest
{
    private readonly SearchController _searchController;
    private readonly ISearchService _searchService;

    public SearchApiTest()
    {
        _searchService = Substitute.For<ISearchService>();
        var logger = Substitute.For<ILogger<SearchController>>();
        _searchController = new SearchController(_searchService, logger);
    }
    [Fact]
    public void SearchController_should_return_ok()
    {
        _searchService.GetData(new SearchRequest{ Genre = "Sci-fi"}).ReturnsForAnyArgs(new List<BookResponse>());
        var response = _searchController.Get(new SearchRequest { Title = "ABC", PublicationYear = 2000});
        _searchService.Received(1).GetData(Arg.Is<SearchRequest>(x => x.Genre == "Scifi"));
    }
}