using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Core.Models;
using Application.Core.Models.Poco;

namespace Application.Core.Interfaces.Repository
{
    public interface ISearchRepository
    {
        List<Book> Search(SearchRequest request);
    }
}