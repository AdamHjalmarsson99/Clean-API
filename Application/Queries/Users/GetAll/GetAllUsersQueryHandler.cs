using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Queries.Users.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<User>>
    {
        private readonly MockDatabase _mockDatabase;

        public GetAllUsersQueryHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<User> allUsersFromMockDatabase = _mockDatabase.Users;
            return Task.FromResult(allUsersFromMockDatabase);
        }
    }
}
