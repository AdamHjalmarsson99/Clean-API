﻿using FluentValidation;

namespace Application.Commands.Dogs.AddDog
{
    public class AddDogCommandValidator : AbstractValidator<AddDogCommand>
    {
        public AddDogCommandValidator()
        {
            RuleFor(x => x.NewDog.Name).NotEmpty().WithMessage("Name can't be null or empty!")
               .Matches("^[a-zA-Z ]*$").WithMessage("The name can only contain letters!");
        }
    }
}
