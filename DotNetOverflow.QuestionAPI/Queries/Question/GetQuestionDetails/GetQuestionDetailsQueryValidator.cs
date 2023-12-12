using FluentValidation;

namespace DotNetOverflow.QuestionAPI.Queries.Question.GetQuestionDetails;

public class GetQuestionDetailsQueryValidator
    : AbstractValidator<GetQuestionDetailsQuery>
{
    public GetQuestionDetailsQueryValidator()
    {
        RuleFor(x =>
                x.Id).NotEqual(Guid.Empty)
            .WithMessage("You don't enter id");
    }
}