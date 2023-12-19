using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repositories.Users;
using MediatR;

namespace Application.Commands.Users.UpdateUser
{
    public class UpdateUserByIdCommandHandler : IRequestHandler<UpdateUserByIdCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserByIdCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Handle(UpdateUserByIdCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await _userRepository.GetById(request.Id);

            if (userToUpdate == null)
                return await Task.FromResult<User>(null!);


            userToUpdate.UserName = request.UpdatedUser.UserName;
            userToUpdate.Password = request.UpdatedUser.Password;

            await _userRepository.Update(userToUpdate);

            return userToUpdate;
        }
    }
}
