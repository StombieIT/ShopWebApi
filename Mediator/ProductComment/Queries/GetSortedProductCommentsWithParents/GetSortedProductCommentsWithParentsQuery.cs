using MediatR;
using ShopWebApi.Models;
using System;
using System.Linq;

namespace ShopWebApi.Mediator
{
    public class GetSortedProductCommentsWithParentsQuery
        : IRequest<IQueryable<Comment<Product>>>
    {
        public Guid ObjectId { get; set; }
        public Guid AuthorId { get; set; }
        public GetSortedProductCommentsWithParentsQuery()
        {}
        public GetSortedProductCommentsWithParentsQuery(Guid objectId, Guid authorId)
        {
            ObjectId = objectId;
            AuthorId = authorId;
        }
    }
}
