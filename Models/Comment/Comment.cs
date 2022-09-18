using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Models
{
    public class Comment<TEntity> where TEntity : class
    {
        public Guid Id { get; set; }
        public TEntity Object { get; set; }
        public User Author { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
    }
}
