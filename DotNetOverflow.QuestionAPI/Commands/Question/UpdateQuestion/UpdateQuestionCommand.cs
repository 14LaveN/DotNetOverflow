using DotNetOverflow.Core.Entity.Image;
using DotNetOverflow.Core.Entity.Question;
using DotNetOverflow.Core.Enum.Question;
using DotNetOverflow.Core.Responses;
using DotNetOverflow.QuestionAPI.DAL.Database.Repository;
using MediatR;

namespace DotNetOverflow.QuestionAPI.Commands.Question.UpdateQuestion;

public class UpdateQuestionCommand
    : IRequest<IBaseResponse<QuestionEntity>>
{
    public required Guid Id { get; set; }

    public required string Body { get; set; }

    public required string Title { get; set; }

    public required long AuthorId { get; set; }

    public required QuestionTypes QuestionType { get; set; }

    public List<ImageEntity> Images { get; set; }

    public required string Tag { get; set; }
}