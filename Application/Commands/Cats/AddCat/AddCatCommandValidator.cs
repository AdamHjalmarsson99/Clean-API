using FluentValidation;

namespace Application.Commands.Cats.AddCat
{
    public class AddCatCommandValidator : AbstractValidator<AddCatCommand>
    {
        public AddCatCommandValidator()
        {
            RuleFor(x => x.NewCat.Name).NotEmpty().WithMessage("Name can't be null or empty!")
                .Matches("^[a-zA-Z ]*$").WithMessage("The name can only contain letters!");

            RuleFor(x => x.NewCat.LikesToPlay).NotNull().WithMessage("LikesToPlay must be specified with true or false!");

            RuleFor(x => x.NewCat.Breed)
               .NotEmpty().WithMessage("Breed can not be null or empty!");

            RuleFor(x => x.NewCat.Weight)
               .NotEmpty().WithMessage("Weight can not be null or empty!");
        }
    }
}
