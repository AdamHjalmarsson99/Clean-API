using Domain.Models;
using Infrastructure.Repositories.Users;
using MediatR;

namespace Application.Queries.Users.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var allUsersFromRealDatabase = await _userRepository.GetAll();
            return allUsersFromRealDatabase;
        }
    }
}
