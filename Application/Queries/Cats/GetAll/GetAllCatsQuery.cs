using Domain.Models;
using MediatR;

namespace Application.Queries.Cats.GetAll
{
    public class GetAllCatsQuery : IRequest<List<Cat>>
    {
        public string? Breed { get; set; }
        public int? Weight { get; set; }
    }
}
