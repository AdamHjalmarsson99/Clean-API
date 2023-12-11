using FluentValidation;

namespace Application.Commands.Cats.UpdateCat
{
    public class UpdateCatByIdCommandValidator : AbstractValidator<UpdateCatByIdCommand>
    {
        public UpdateCatByIdCommandValidator()
        {

            RuleFor(x => x.UpdatedCat.Name).NotEmpty().WithMessage("Name can't be null or empty!")
                .Matches("^[a-zA-Z ]*$").WithMessage("The name can only contain letters!");

            RuleFor(x => x.UpdatedCat.LikesToPlay).NotNull().WithMessage("LikesToPlay must be specified with true or false!");
        }

    }
}
