using Domain.Models;
using Infrastructure.Repositories.Animals;
using Infrastructure.Repositories.Users;
using MediatR;

namespace Application.Commands.Users.ConnectAnimalToUser
{
    public class ConnectAnimalToUserCommandHandler : IRequestHandler<ConnectAnimalToUserCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAnimalRepository _animalRepository;

        public ConnectAnimalToUserCommandHandler(IUserRepository userRepository, IAnimalRepository animalRepository)
        {
            _userRepository = userRepository;
            _animalRepository = animalRepository;
        }

        public async Task<User> Handle(ConnectAnimalToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserConnectedWithAnimal.UserId);

            if (user == null)
            {
                return await Task.FromResult<User>(null!);
            }

            var animal = await _animalRepository.GetAnimalById(request.UserConnectedWithAnimal.AnimalId);

            if (animal == null)
            {
                return null!;
            }

            user.Animals.Add(animal);
            await _userRepository.Update(user);

            return user;
        }
    }
}
