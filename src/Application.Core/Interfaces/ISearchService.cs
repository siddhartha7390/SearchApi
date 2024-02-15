using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Core.Models;

namespace Application.Core.Interfaces
{
    public interface ISearchService
    {
        List<BookResponse> GetData(SearchRequest request);
    }    
}
