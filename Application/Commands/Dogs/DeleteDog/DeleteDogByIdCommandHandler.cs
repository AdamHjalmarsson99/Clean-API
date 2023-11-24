using Domain.Models;
using MediatR;
using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogByIdCommandHandler : IRequestHandler<DeleteDogByIdCommand, Dog>
    {
        private readonly MockDatabase _mockDatabase;

        public DeleteDogByIdCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<Dog> Handle(DeleteDogByIdCommand request, CancellationToken cancellationToken)
        {
            Dog dogToDelete = _mockDatabase.Dogs.FirstOrDefault(dog => dog.Id == request.Id)!;

            if (dogToDelete == null)
            {
                return Task.FromResult<Dog>(null!);
            }

            _mockDatabase.Dogs.Remove(dogToDelete);

            return Task.FromResult(dogToDelete);
        }
    }
}
