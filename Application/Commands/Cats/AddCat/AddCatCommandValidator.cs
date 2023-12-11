using FluentValidation;

namespace Application.Commands.Cats.AddCat
{
    public class AddCatCommandValidator : AbstractValidator<AddCatCommand>
    {
        public AddCatCommandValidator()
        {
            RuleFor(x => x.NewCat.Name).NotEmpty().WithMessage("Name can't be null or empty!")
                .Matches("^[a-zA-Z ]*$").WithMessage("The name can only contain letters!");

            RuleFor(x => x.NewCat.LikesToPlay).NotEmpty().WithMessage("LikesToPlay must be specified with true or false!");
        }
    }
}
