using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Users.AddNewUsers
{
    public class AddNewUserCommandHandler : IRequestHandler<AddNewUserCommand, User>
    {
        private readonly MockDatabase _mockDatabase;

        public AddNewUserCommandHandler()
        {
            _mockDatabase = new MockDatabase();
        }

        public Task<User> Handle(AddNewUserCommand request, CancellationToken cancellationToken)
        {
            User userToCreate = new()
            {
                Id = Guid.NewGuid(),
                UserName = request.AddNewUser.UserName,
                Password = request.AddNewUser.Password
            };

            _mockDatabase.Users.Add(userToCreate);

            return Task.FromResult(userToCreate);
        }
    }


}
