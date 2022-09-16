using System;

namespace ShopWebApi.Models
{
    public class Image<TEntity>
        where TEntity : class
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public TEntity Object { get; set; }
    }
}
