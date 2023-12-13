using Domain.Models;
using MediatR;

namespace Application.Queries.Users.GetById
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public GetUserByIdQuery(Guid userId)
        {
            Id = userId;
        }
        public Guid Id { get; }
    }
}
