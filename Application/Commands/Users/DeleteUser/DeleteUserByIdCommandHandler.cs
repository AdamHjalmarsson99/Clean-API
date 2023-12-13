using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Users.DeleteUser
{
    public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, User>
    {
        private readonly MockDatabase _mockDatabase;

        public DeleteUserByIdCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<User> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            User userToDelete = _mockDatabase.Users.FirstOrDefault(user => user.Id == request.Id)!;

            if (userToDelete == null)
            {
                return Task.FromResult<User>(null!);
            }

            _mockDatabase.Users.Remove(userToDelete);

            return Task.FromResult(userToDelete);
        }
    }
}
