using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Users.UpdateUser
{
    public class UpdateUserByIdCommand : IRequest<User>
    {
        public UpdateUserByIdCommand(UserDto updatedUser, Guid id)
        {
            UpdatedUser = updatedUser;
            Id = id;
        }
        public UserDto UpdatedUser { get; }
        public Guid Id { get; }
    }
}
