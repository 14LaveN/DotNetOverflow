using DotNetOverflow.Core.Entity.Question;

namespace DotNetOverflow.Core.Entity.Image;

public class ImageEntity
{
    public Guid Id { get; set; }

    public required string FileName { get; set; }

    public required byte[] Image { get; set; }

    public required Guid QuestionId { get; set; }

    public QuestionEntity? Question { get; set; }
}