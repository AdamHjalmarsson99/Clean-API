using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Users.ConnectAnimalToUser
{
    public class ConnectAnimalToUserCommand : IRequest<User>
    {
        public ConnectAnimalToUserCommand(UserAnimalDto userAnimal)
        {
            UserConnectedWithAnimal = userAnimal;
        }

        public UserAnimalDto UserConnectedWithAnimal { get; }
    }
}
