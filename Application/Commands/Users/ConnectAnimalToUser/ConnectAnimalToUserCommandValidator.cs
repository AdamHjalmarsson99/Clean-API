using FluentValidation;

namespace Application.Commands.Users.ConnectAnimalToUser
{
    public class ConnectAnimalToUserCommandValidator : AbstractValidator<ConnectAnimalToUserCommand>
    {
        public ConnectAnimalToUserCommandValidator()
        {
            RuleFor(x => x.UserConnectedWithAnimal.UserId)
                    .NotEmpty().WithMessage("UserId cannot be null or empty");

            RuleFor(x => x.UserConnectedWithAnimal.AnimalId)
                .NotEmpty().WithMessage("AnimalId cannot be null or empty");
        }
    }
}
