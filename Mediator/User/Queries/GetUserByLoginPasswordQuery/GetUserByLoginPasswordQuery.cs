using MediatR;
using ShopWebApi.Models;

namespace ShopWebApi.Mediator
{
    public class GetUserByLoginPasswordQuery : IRequest<User>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public GetUserByLoginPasswordQuery()
        {}
        public GetUserByLoginPasswordQuery(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
