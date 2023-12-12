using DotNetOverflow.Core.Entity.Image;
using DotNetOverflow.Core.Entity.Question;
using DotNetOverflow.Core.Enum.Question;
using DotNetOverflow.Core.Responses;
using MediatR;

namespace DotNetOverflow.QuestionAPI.Commands.Question.CreateQuestion;

public class CreateQuestionCommand
    : IRequest<IBaseResponse<QuestionEntity>>

{
    public required string Body { get; set; }

    public required string Title { get; set; }

    public required QuestionTypes QuestionType { get; set; }

    public required long AuthorId { get; set; }

    public required string Tag { get; set; }

    public static implicit operator QuestionEntity(CreateQuestionCommand createQuestionCommand)
    {
        return new QuestionEntity
        {
            AuthorId = createQuestionCommand.AuthorId,
            QuestionType = createQuestionCommand.QuestionType,
            Tag = createQuestionCommand.Tag,
            Title = createQuestionCommand.Title,
            Body = createQuestionCommand.Body,
            LikesCount = 0,
            CreatedAt = DateTime.UtcNow,
            CommentsCount = 0
        };
    }
}