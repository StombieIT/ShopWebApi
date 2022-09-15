using MediatR;
using System;

namespace ShopWebApi.Mediator
{
    public class RemoveProductCommentCommand
        : IRequest
    {
        public Guid ProductCommentId { get; set; }
        public RemoveProductCommentCommand()
        {}
        public RemoveProductCommentCommand(Guid productCommentId)
        {
            ProductCommentId = productCommentId;
        }
    }
}
