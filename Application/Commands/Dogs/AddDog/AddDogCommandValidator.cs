using FluentValidation;

namespace Application.Commands.Dogs.AddDog
{
    public class AddDogCommandValidator : AbstractValidator<AddDogCommand>
    {
        public AddDogCommandValidator()
        {
            RuleFor(x => x.NewDog.Name).NotEmpty().WithMessage("Name can't be null or empty!")
               .Matches("^[a-zA-Z ]*$").WithMessage("The name can only contain letters!");

            RuleFor(x => x.NewDog.Breed)
               .NotEmpty().WithMessage("Breed can not be null or empty!");

            RuleFor(x => x.NewDog.Weight)
               .NotEmpty().WithMessage("Weight can not be null or empty!");
        }
    }
}
