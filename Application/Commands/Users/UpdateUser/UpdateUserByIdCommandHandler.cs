using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Users.UpdateUser
{
    public class UpdateUserByIdCommandHandler : IRequestHandler<UpdateUserByIdCommand, User>
    {
        private readonly MockDatabase _mockDatabase;

        public UpdateUserByIdCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public Task<User> Handle(UpdateUserByIdCommand request, CancellationToken cancellationToken)
        {
            User userToUpdate = _mockDatabase.Users.FirstOrDefault(user => user.Id == request.Id)!;

            if (userToUpdate == null)
                return Task.FromResult<User>(null!);


            userToUpdate.UserName = request.UpdatedUser.UserName;
            userToUpdate.Password = request.UpdatedUser.Password;

            return Task.FromResult(userToUpdate);
        }
    }


}
