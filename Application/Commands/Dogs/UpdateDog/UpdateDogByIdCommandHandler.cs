using Domain.Models;
using Infrastructure.Repositories.Dogs;
using MediatR;

namespace Application.Commands.Dogs.UpdateDog
{
    public class UpdateDogByIdCommandHandler : IRequestHandler<UpdateDogByIdCommand, Dog>
    {
        private readonly IDogRepository _dogRepository;

        public UpdateDogByIdCommandHandler(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }
        public async Task<Dog> Handle(UpdateDogByIdCommand request, CancellationToken cancellationToken)
        {
            var dogToUpdate = await _dogRepository.GetById(request.Id);

            if (dogToUpdate == null)
                return await Task.FromResult<Dog>(null!);


            dogToUpdate.Name = request.UpdatedDog.Name;

            await _dogRepository.Update(dogToUpdate);

            return dogToUpdate;
        }
    }
}
