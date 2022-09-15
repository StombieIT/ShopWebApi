using MediatR;
using ShopWebApi.Models;

namespace ShopWebApi.Mediator
{
    public class AddUserCommand : IRequest<User>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public AddUserCommand()
        {}
        public AddUserCommand(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
