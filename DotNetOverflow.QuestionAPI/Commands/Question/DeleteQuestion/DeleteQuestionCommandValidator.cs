using FluentValidation;

namespace DotNetOverflow.QuestionAPI.Commands.Question.DeleteQuestion;

public class DeleteQuestionCommandValidator
    : AbstractValidator<DeleteQuestionCommand>
{
    public DeleteQuestionCommandValidator()
    {
        RuleFor(x =>
                x.Id).NotEqual(Guid.Empty)
            .WithMessage("You don't enter id");
    }
}