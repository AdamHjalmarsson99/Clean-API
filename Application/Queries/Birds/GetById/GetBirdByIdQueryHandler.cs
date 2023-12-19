using Domain.Models;
using Infrastructure.Repositories.Birds;
using MediatR;

namespace Application.Queries.Birds.GetById
{
    public class GetBirdByIdQueryHandler : IRequestHandler<GetBirdByIdQuery, Bird?>
    {
        private readonly IBirdRepository _birdRepository;

        public GetBirdByIdQueryHandler(IBirdRepository birdRepository)
        {
            _birdRepository = birdRepository;
        }

        public async Task<Bird?> Handle(GetBirdByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                return null;
            }

            return await _birdRepository.GetById(request.Id);
        }

    }
}
