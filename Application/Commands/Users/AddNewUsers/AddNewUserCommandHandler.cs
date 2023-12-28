using Domain.Models;
using Domain.Models.Animal;
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
                Password = request.AddNewUser.Password,
                Animals = new List<AnimalModel>()
            };

            if (request.AddNewUser.Dogs != null)
            {
                var dogs = request.AddNewUser.Dogs.Select(d => new Dog { Name = d.Name, Breed = d.Breed, Weight = d.Weight, Users = new List<User> { userToCreate } });
                userToCreate.Animals = userToCreate.Animals.Concat(dogs).ToList();
            }

            if (request.AddNewUser.Cats != null)
            {
                var cats = request.AddNewUser.Cats.Select(c => new Cat { Name = c.Name, Breed = c.Breed, LikesToPlay = c.LikesToPlay, Weight = c.Weight, Users = new List<User> { userToCreate } });
                userToCreate.Animals = userToCreate.Animals.Concat(cats).ToList();
            }

            if (request.AddNewUser.Birds != null)
            {
                var birds = request.AddNewUser.Birds.Select(b => new Bird { Name = b.Name, Color = b.Color, CanFly = b.CanFly, Users = new List<User> { userToCreate } });
                userToCreate.Animals = userToCreate.Animals.Concat(birds).ToList();
            }

            await _userRepository.Add(userToCreate);

            return userToCreate;
        }
    }
}
