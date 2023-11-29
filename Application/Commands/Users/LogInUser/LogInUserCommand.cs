using Application.Dtos;
using MediatR;

namespace Application.Commands.Users.LogInUser
{
    public sealed class LogInUserCommand : IRequest<string>
    {
        public LogInUserCommand(UserDto userLogIn)
        {
            UserLogIn = userLogIn;
        }
        public UserDto UserLogIn { get; }
    }
}
