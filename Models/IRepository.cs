using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Models
{
    public interface IRepository
    {
        IQueryable<TEntity> CreateQuery<TEntity>() where TEntity : class;
        void Add<TEntity>(TEntity item);
        void Remove<TEntity>(TEntity item);
        void Update<TEntity>(TEntity item);
    }
}
