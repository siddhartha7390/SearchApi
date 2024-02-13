using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using Application.Services;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly List<string> sampleData = new List<string>
    {
        "Data 1",
        "Data 2",
        "Data 3"
    };
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        return _searchService.GetData();
    }

    [HttpGet("{id}")]
    public ActionResult<string> GetById(int id)
    {
        if (id >= 0 && id < sampleData.Count)
        {
            return Ok(sampleData[id]);
        }

        return NotFound("Item not found");
    }
}
