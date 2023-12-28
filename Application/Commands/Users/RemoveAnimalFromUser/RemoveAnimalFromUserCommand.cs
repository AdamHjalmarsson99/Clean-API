using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Users.RemoveAnimalFromUser
{
    public class RemoveAnimalFromUserCommand : IRequest<User>
    {
        public RemoveAnimalFromUserCommand(UserAnimalDto removeAnimalFromUser)
        {
            RemoveAnimalFromUser = removeAnimalFromUser;
        }

        public UserAnimalDto RemoveAnimalFromUser { get; }
    }
}
