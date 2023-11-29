using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Users.AddNewUsers
{
    public class AddNewUserCommand : IRequest<User>
    {
        public AddNewUserCommand(UserDto addNewUser)
        {
            AddNewUser = addNewUser;
        }
        public UserDto AddNewUser { get; }
    }
}
