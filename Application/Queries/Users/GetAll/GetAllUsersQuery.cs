using Domain.Models;
using MediatR;

namespace Application.Queries.Users.GetAll
{
    public class GetAllUsersQuery : IRequest<List<User>>
    {
    }
}
