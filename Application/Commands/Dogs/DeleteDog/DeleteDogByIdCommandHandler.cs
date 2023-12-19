using Domain.Models;
using MediatR;
using Infrastructure.Repositories.Dogs;

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogByIdCommandHandler : IRequestHandler<DeleteDogByIdCommand, Dog>
    {
        private readonly IDogRepository _dogRepository;

        public DeleteDogByIdCommandHandler(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }

        public async Task<Dog> Handle(DeleteDogByIdCommand request, CancellationToken cancellationToken)
        {
            var dogToDelete = await _dogRepository.GetById(request.Id);

            if (dogToDelete == null)
            {
                return await Task.FromResult<Dog>(null!);
            }

            await _dogRepository.Delete(dogToDelete);

            return dogToDelete;
        }
    }
}
