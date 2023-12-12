using FluentValidation;

namespace DotNetOverflow.Identity.Commands.Register;

public class RegisterCommandValidator
    : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(registerCommand =>
                registerCommand.UserName).NotEqual(String.Empty)
            .WithMessage("You don't enter a user name")
            .MaximumLength(28)
            .WithMessage("Your user name is too big");
        
        RuleFor(registerCommand =>
                registerCommand.RetypePassword).NotEqual(String.Empty)
            .WithMessage("You don't enter a password")
            .MaximumLength(36)
            .WithMessage("Your password is too big");

        RuleFor(registerCommand =>
                registerCommand.Email).NotEqual(String.Empty)
            .WithMessage("You don't enter a password")
            .EmailAddress();
        
        RuleFor(registerCommand =>
                registerCommand.Firstname).NotEqual(String.Empty)
            .WithMessage("You don't enter a first name")
            .MaximumLength(36)
            .WithMessage("Your first name is too big");
        
        RuleFor(registerCommand =>
                registerCommand.Lastname).NotEqual(String.Empty)
            .WithMessage("You don't enter a last name")
            .MaximumLength(36)
            .WithMessage("Your last name is too big");
        
        RuleFor(registerCommand =>
                registerCommand.Patronymic).NotEqual(String.Empty)
            .WithMessage("You don't enter a patronymic")
            .MaximumLength(36)
            .WithMessage("Your patronymic is too big");
    }
}