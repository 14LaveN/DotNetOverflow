using DotNetOverflow.Core.Entity.Question;
using DotNetOverflow.Core.Responses;
using MediatR;

namespace DotNetOverflow.QuestionAPI.Queries.Question.GetQuestionDetails;

public class GetQuestionDetailsQuery
    : IRequest<IBaseResponse<QuestionEntity>>
{
    public required Guid Id { get; set; }

    public static implicit operator GetQuestionDetailsQuery(Guid id)
    {
        return new GetQuestionDetailsQuery
        {
            Id = id
        };
    }
}