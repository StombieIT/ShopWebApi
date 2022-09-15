using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Exceptions;
using ShopWebApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebApi.Mediator
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
    {
        private readonly ApplicationDbContext dbContext;

        public AddUserCommandHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> Handle(AddUserCommand command, CancellationToken cancellationToken)
        {
            User user = await dbContext
                .Users
                .FirstOrDefaultAsync(
                    u =>
                        u.Login == command.Login,
                    cancellationToken
                );
            if (user != null)
                throw new AlreadyExistsException($"User with login '{command.Login}' already exists");
            user = new User
            {
                Login = command.Login,
                Password = command.Password
            };
            await dbContext.AddAsync(user, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return user;
        }
    }
}
