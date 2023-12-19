using Domain.Models;
using Infrastructure.Repositories.Users;
using MediatR;

namespace Application.Commands.Users.AddNewUsers
{
    public class AddNewUserCommandHandler : IRequestHandler<AddNewUserCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public AddNewUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(AddNewUserCommand request, CancellationToken cancellationToken)
        {
            User userToCreate = new()
            {
                Id = Guid.NewGuid(),
                UserName = request.AddNewUser.UserName,
                Password = request.AddNewUser.Password
            };

            await _userRepository.Add(userToCreate);

            return userToCreate;
        }
    }
}
