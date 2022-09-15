using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Utils
{
    public class Paginator<T> : IPaginator<T> where T : class
    {
        private readonly int page;
        private readonly int limit;
        private readonly int pagesCount;
        private readonly IQueryable<T> items;

        public int Page => page;
        public bool PageExists => 1 <= page && page <= pagesCount;
        public bool HasNextPage => 0 <= page && page < pagesCount;
        public bool HasPreviousPage => 1 < page && page <= pagesCount + 1;
        public IQueryable<T> PageItems => page > 0 && limit > 0
            ? items.Skip((page - 1) * limit).Take(limit)
            : Enumerable.Empty<T>().AsQueryable();

        public Paginator(int page, int limit, IQueryable<T> items)
        {
            this.page = page;
            this.limit = limit;
            this.items = items;
            pagesCount = (int)Math.Ceiling((double)items.Count() / limit);
        }
    }
}
