using Domain.Models;
using Infrastructure.Repositories.Cats;
using MediatR;

namespace Application.Queries.Cats.GetAll
{
    public class GetAllCatsQueryHandler : IRequestHandler<GetAllCatsQuery, List<Cat>>
    {
        private readonly ICatRepository _catRepository;

        public GetAllCatsQueryHandler(ICatRepository catRepository)
        {
            _catRepository = catRepository;
        }
        public async Task<List<Cat>> Handle(GetAllCatsQuery request, CancellationToken cancellationToken)
        {
            var allCatsFromRealDatabase = await _catRepository.GetAll(request.Breed, request.Weight);
            return allCatsFromRealDatabase;
        }
    }
}
