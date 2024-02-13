using System;
using System.Collections.Generic;
using Application.Interfaces;
namespace Application.Services
{
    public class SearchService:ISearchService
    {
        public SearchService()
        {

        }

        public List<string> GetData()
        {
            return new List<string>
            {
                "Data 1",
                "Data 2",
                "Data 3"
            };
        }
    }
}