using FluentValidation;

namespace DotNetOverflow.ImageAPI.Commands.Image.CreateImages;

public class CreateImagesCommandValidator
    : AbstractValidator<CreateImagesCommand>
{
    public CreateImagesCommandValidator()
    {
        RuleFor(x =>
                x.QuestionId).NotEqual(Guid.Empty)
            .WithMessage("You don't enter question id");
    }
}