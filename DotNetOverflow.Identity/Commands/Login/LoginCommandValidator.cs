using FluentValidation;

namespace DotNetOverflow.Identity.Commands.Login;

public class LoginCommandValidator
    : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(loginCommand =>
                loginCommand.UserName).NotEqual(String.Empty)
            .WithMessage("You don't enter a user name")
            .MaximumLength(28)
            .WithMessage("Your user name is too big");
        
        RuleFor(loginCommand =>
                loginCommand.Password).NotEqual(String.Empty)
            .WithMessage("You don't enter a password")
            .MaximumLength(36)
            .WithMessage("Your password is too big");
    }
}