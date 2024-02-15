using Microsoft.AspNetCore.Mvc;
using Application.Core.Interfaces;
using Application.Core.Models;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<BookResponse>> Get([FromQuery] SearchRequest request)
    {
        SearchRequestValidator validator = new();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }
        request.Title = CorrectTypos.CorrectTypo(request.Title);
        return Ok(_searchService.GetData(request));
    }
}
