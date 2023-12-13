using FluentValidation;

namespace Application.Commands.Users.UpdateUser
{
    public class UpdateUserByIdCommandValidator : AbstractValidator<UpdateUserByIdCommand>
    {
        public UpdateUserByIdCommandValidator()
        {
            RuleFor(x => x.UpdatedUser.UserName).NotEmpty().WithMessage("Username can't be null or empty!")
               .Matches("^[a-zA-Z0-9_-]+$").WithMessage("Username can only contain letters, numbers, underscores, and hyphens.");

            RuleFor(x => x.UpdatedUser.Password).NotEmpty().WithMessage("Password can't be empty or null")
                .MinimumLength(4).WithMessage("Password must contain four or more symbols!");
        }
    }
}
