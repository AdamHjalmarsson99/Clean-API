using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Users.AddNewUsers
{
    public class AddNewUserCommand : IRequest<User>
    {
        public AddNewUserCommand(CreateUserDto addNewUser)
        {
            AddNewUser = addNewUser;
        }
        public CreateUserDto AddNewUser { get; }
    }
}
