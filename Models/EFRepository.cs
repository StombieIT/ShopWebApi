using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Models
{
    internal class EFRepository : IRepository
    {
        private readonly ApplicationDbContext context;

        public EFRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IQueryable<TEntity> CreateQuery<TEntity>() where TEntity : class =>
            context.Set<TEntity>();
        public void Add<TEntity>(TEntity item)
        {
            context.Add(item);
            context.SaveChanges();
        }
        public void Remove<TEntity>(TEntity item)
        {
            context.Remove(item);
            context.SaveChanges();
        }
        public void Update<TEntity>(TEntity item)
        {
            context.Update(item);
            context.SaveChanges();
        }
    }
}
