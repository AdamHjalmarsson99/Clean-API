using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Dogs.UpdateDog
{
    public class UpdateDogByIdCommandValidator : AbstractValidator<UpdateDogByIdCommand>
    {
        public UpdateDogByIdCommandValidator()
        {
            RuleFor(x => x.UpdatedDog.Name).NotEmpty().WithMessage("Name can't be null or empty!")
               .Matches("^[a-zA-Z ]*$").WithMessage("The name can only contain letters!");
        }
    }
}
