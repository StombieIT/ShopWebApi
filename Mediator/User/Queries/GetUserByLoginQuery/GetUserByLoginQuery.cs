using MediatR;
using ShopWebApi.Models;

namespace ShopWebApi.Mediator
{
    public class GetUserByLoginQuery : IRequest<User>
    {
        public string Login { get; set; }
        public GetUserByLoginQuery(string login)
        {
            Login = login;
        }
        public GetUserByLoginQuery()
        {}
    }
}
