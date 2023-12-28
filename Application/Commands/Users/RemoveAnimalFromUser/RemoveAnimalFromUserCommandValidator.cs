using FluentValidation;

namespace Application.Commands.Users.RemoveAnimalFromUser
{
    public class RemoveAnimalFromUserCommandValidator : AbstractValidator<RemoveAnimalFromUserCommand>
    {
        public RemoveAnimalFromUserCommandValidator()
        {
            RuleFor(x => x.RemoveAnimalFromUser.UserId)
                        .NotEmpty().WithMessage("UserId cannot be null or empty");

            RuleFor(x => x.RemoveAnimalFromUser.AnimalId)
                    .NotEmpty().WithMessage("AnimalId cannot be null or empty");
        }
    }
}
