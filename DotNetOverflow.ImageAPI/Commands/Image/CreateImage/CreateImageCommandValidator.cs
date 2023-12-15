using FluentValidation;

namespace DotNetOverflow.ImageAPI.Commands.Image.CreateImage;

public sealed class CreateImageCommandValidator
    : AbstractValidator<CreateImageCommand>
{
    public CreateImageCommandValidator()
    {
        RuleFor(x =>
                x.Image).NotNull()
            .WithMessage("You don't choose image");
    }
}