using Domain.Models;
using Infrastructure.Repositories.Cats;
using MediatR;

namespace Application.Commands.Cats.UpdateCat
{
    public class UpdateCatByIdCommandHandler : IRequestHandler<UpdateCatByIdCommand, Cat>
    {
        private readonly ICatRepository _catRepository;

        public UpdateCatByIdCommandHandler(ICatRepository catRepository)
        {
            _catRepository = catRepository;
        }
        public async Task<Cat> Handle(UpdateCatByIdCommand request, CancellationToken cancellationToken)
        {
            var catToUpdate = await _catRepository.GetById(request.Id);

            if (catToUpdate == null)
                return await Task.FromResult<Cat>(null!);


            catToUpdate.Name = request.UpdatedCat.Name;
            catToUpdate.LikesToPlay = request.UpdatedCat.LikesToPlay;
            catToUpdate.Breed = request.UpdatedCat.Breed;
            catToUpdate.Weight = request.UpdatedCat.Weight;

            await _catRepository.Update(catToUpdate);

            return catToUpdate;
        }
    }
}
