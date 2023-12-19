using Domain.Models;
using Infrastructure.Repositories.Cats;
using MediatR;

namespace Application.Queries.Cats.GetById
{
    public class GetCatByIdQueryHandler : IRequestHandler<GetCatByIdQuery, Cat?>
    {
        private readonly ICatRepository _catRepository;

        public GetCatByIdQueryHandler(ICatRepository catRepository)
        {
            _catRepository = catRepository;
        }

        public async Task<Cat?> Handle(GetCatByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                return null;
            }

            return await _catRepository.GetById(request.Id);
        }
    }
}
