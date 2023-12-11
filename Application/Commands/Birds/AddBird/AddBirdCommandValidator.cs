using FluentValidation;

namespace Application.Commands.Birds.AddBird
{
    public class AddBirdCommandValidator : AbstractValidator<AddBirdCommand>
    {
        public AddBirdCommandValidator() 
        {
            RuleFor(x => x.NewBird.Name)
                .NotEmpty().WithMessage("Name cannot be null or empty!")
                .Matches("^[a-zA-Z ]*$").WithMessage("The name can only contain letters!");

            RuleFor(x => x.NewBird.CanFly)
               .NotNull().WithMessage("CanFly must be specified with true or false!");
        }
    }
}
