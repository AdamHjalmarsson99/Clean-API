using FluentValidation;

namespace Application.Commands.Birds.UpdateBird
{
    public class UpdateBirdByIdCommandValidator : AbstractValidator<UpdateBirdByIdCommand>
    {
        public UpdateBirdByIdCommandValidator()
        {
            RuleFor(x => x.UpdatedBird.Name).NotEmpty().WithMessage("Name can't be null or empty!")
                .Matches("^[a-zA-Z ]*$").WithMessage("The name can only contain letters!");

            RuleFor(x => x.UpdatedBird.CanFly).NotNull().WithMessage("CanFly must be specified with true or false!");

            RuleFor(x => x.UpdatedBird.Color)
               .NotEmpty().WithMessage("Color can not be null or empty!");
        }
    }
}
