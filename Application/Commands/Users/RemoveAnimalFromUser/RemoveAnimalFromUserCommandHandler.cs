using Domain.Models;
using Infrastructure.Repositories.Animals;
using Infrastructure.Repositories.Users;
using MediatR;

namespace Application.Commands.Users.RemoveAnimalFromUser
{
    public class RemoveAnimalFromUserCommandHandler : IRequestHandler<RemoveAnimalFromUserCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAnimalRepository _animalRepository;

        public RemoveAnimalFromUserCommandHandler(IUserRepository userRepository, IAnimalRepository animalRepository)
        {
            _userRepository = userRepository;
            _animalRepository = animalRepository;
        }

        public async Task<User> Handle(RemoveAnimalFromUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.RemoveAnimalFromUser.UserId);

            if (user == null)
            {
                return await Task.FromResult<User>(null!);
            }

            var animalToRemove = await _animalRepository.GetAnimalById(request.RemoveAnimalFromUser.AnimalId);

            if (animalToRemove == null)
            {
                return null!;
            }

            user.Animals.Remove(animalToRemove);
            await _userRepository.Update(user);

            return user;
        }
    }
}
