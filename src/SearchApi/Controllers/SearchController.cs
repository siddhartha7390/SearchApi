using Microsoft.AspNetCore.Mvc;
using Application.Core.Interfaces;
using Application.Core.Models;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;
    private ILogger<SearchController> _logger;

    public SearchController(ISearchService searchService, ILogger<SearchController> logger)
    {
        _logger = logger;
        _searchService = searchService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<BookResponse>> Get([FromQuery] SearchRequest request)
    {
        SearchRequestValidator validator = new();
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            _logger.LogError("Data validation failed for search input");
            return BadRequest(result.Errors);
        }
        request.Genre = request.Genre != null ? CorrectTypos.CorrectTypo(request.Genre).ToLower() : request.Genre;
        request.Title = request.Title?.ToLower();
        request.Type = request.Type?.ToLower();
        request.AuthorName = request.AuthorName?.ToLower();
        return Ok(_searchService.GetData(request));
    }
}
