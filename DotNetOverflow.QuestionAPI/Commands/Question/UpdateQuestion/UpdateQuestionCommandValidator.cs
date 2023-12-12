using FluentValidation;

namespace DotNetOverflow.QuestionAPI.Commands.Question.UpdateQuestion;

public class UpdateQuestionCommandValidator
    : AbstractValidator<UpdateQuestionCommand>
{
    public UpdateQuestionCommandValidator()
    {
        RuleFor(x =>
                x.Title).NotEqual(String.Empty)
            .WithMessage("You don't enter title")
            .MaximumLength(95)
            .WithMessage("Your title too big");
        
        RuleFor(x =>
                x.Body).NotEqual(String.Empty)
            .WithMessage("You don't enter body")
            .MaximumLength(512)
            .WithMessage("Your body too big");

        RuleFor(x =>
                x.Id).NotEqual(Guid.Empty)
            .WithMessage("You don't enter guid");
        
        RuleFor(x =>
                x.Tag).MaximumLength(95)
            .WithMessage("Your tag too big");
    }
}