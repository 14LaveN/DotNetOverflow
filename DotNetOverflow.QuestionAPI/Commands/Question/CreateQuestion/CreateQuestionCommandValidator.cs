using FluentValidation;

namespace DotNetOverflow.QuestionAPI.Commands.Question.CreateQuestion;

public class CreateQuestionCommandValidator
    : AbstractValidator<CreateQuestionCommand>
{
    public CreateQuestionCommandValidator()
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
                x.AuthorId).NotEqual(0)
            .WithMessage("You don't enter author");
        
        RuleFor(x =>
                x.Tag).MaximumLength(95)
            .WithMessage("Your tag too big");
    }
}