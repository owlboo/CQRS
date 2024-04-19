using CQRS.Application.Commands.User;
using FluentValidation;

namespace CQRS.Application.Validators.User
{
    public class AddUserCommandValidator : AbstractValidator<CommandAddUser>
    {
        public AddUserCommandValidator()
        {
            RuleFor(c => c.Email).NotEmpty()
                .WithMessage("The email can not be empty")
                .EmailAddress()
                .WithMessage("The email is not valid");

            RuleFor(c => c.Username)
                .NotEmpty()
                .WithMessage("The username can not be empty")
                .MinimumLength(5)
                .MaximumLength(10)
                .WithMessage("The length of username shoule be between 5 to 10");

            RuleFor(c => c.Address)
                .NotEmpty()
                .WithMessage("The address can not be empty");
        }
    }
}
