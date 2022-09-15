using System.Collections.Generic;
using System.Linq;

namespace ShopWebApi.Utils
{
    public interface IPaginator<T> where T : class
    {
        int Page { get; }
        bool HasNextPage { get; }
        bool HasPreviousPage { get; }
        bool PageExists { get; }
        IQueryable<T> PageItems { get; }
    }
}
