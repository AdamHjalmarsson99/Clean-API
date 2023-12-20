using FluentValidation;

namespace Application.Commands.Dogs.UpdateDog
{
    public class UpdateDogByIdCommandValidator : AbstractValidator<UpdateDogByIdCommand>
    {
        public UpdateDogByIdCommandValidator()
        {
            RuleFor(x => x.UpdatedDog.Name).NotEmpty().WithMessage("Name can't be null or empty!")
               .Matches("^[a-zA-Z ]*$").WithMessage("The name can only contain letters!");

            RuleFor(x => x.UpdatedDog.Breed)
               .NotEmpty().WithMessage("Breed can not be null or empty!");

            RuleFor(x => x.UpdatedDog.Weight)
               .NotEmpty().WithMessage("Weight can not be null or empty!");
        }
    }
}
