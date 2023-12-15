using DotNetOverflow.Core.Responses;
using MediatR;

namespace DotNetOverflow.QuestionAPI.Commands.Question.DeleteQuestion;

public class DeleteQuestionCommand
    : IRequest<IBaseResponse<DeleteQuestionCommand>>
{
    public required Guid Id { get; init; }
    
    public required string Author { get; set; }
}