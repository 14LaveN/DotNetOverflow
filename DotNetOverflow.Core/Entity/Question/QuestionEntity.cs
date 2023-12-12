using System.Collections;
using DotNetOverflow.Core.Entity.Account;
using DotNetOverflow.Core.Entity.Image;
using DotNetOverflow.Core.Enum.Question;

namespace DotNetOverflow.Core.Entity.Question;

public class QuestionEntity
{
    public Guid Id { get; set; }

    public required string Body { get; set; }

    public required string Title { get; set; }

    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public long LikesCount { get; set; }

    public long CommentsCount { get; set; }

    public required QuestionTypes QuestionType { get; set; }

    public string Tag { get; set; }

    public required long AuthorId { get; set; }

    public ICollection<ImageEntity>? Images { get; set; }

    public AppUser? AppUser { get; set; }
}