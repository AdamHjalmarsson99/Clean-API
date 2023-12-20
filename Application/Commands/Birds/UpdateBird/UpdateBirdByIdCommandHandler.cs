using Domain.Models;
using Infrastructure.Repositories.Birds;
using MediatR;

namespace Application.Commands.Birds.UpdateBird
{
    public class UpdateBirdByIdCommandHandler : IRequestHandler<UpdateBirdByIdCommand, Bird>
    {
        private readonly IBirdRepository _birdRepository;

        public UpdateBirdByIdCommandHandler(IBirdRepository birdRepository)
        {
            _birdRepository = birdRepository;
        }

        public async Task<Bird> Handle(UpdateBirdByIdCommand request, CancellationToken cancellationToken)
        {
            var birdToUpdate = await _birdRepository.GetById(request.Id);

            if (birdToUpdate == null)
                return await Task.FromResult<Bird>(null!);


            birdToUpdate.Name = request.UpdatedBird.Name;
            birdToUpdate.CanFly = request.UpdatedBird.CanFly;
            birdToUpdate.Color = request.UpdatedBird.Color;

            await _birdRepository.Update(birdToUpdate);

            return birdToUpdate;
        }

    }
}
