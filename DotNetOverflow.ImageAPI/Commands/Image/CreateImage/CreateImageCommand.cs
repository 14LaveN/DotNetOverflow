using DotNetOverflow.Core.Entity.Image;
using DotNetOverflow.Core.Responses;
using MediatR;

namespace DotNetOverflow.ImageAPI.Commands.Image.CreateImage;

public class CreateImageCommand
    : IRequest<IBaseResponse<ImageEntity>>
{
    public required byte[] Image { get; set; }

    public required Guid QuestionId { get; set; }

    public static implicit operator ImageEntity(CreateImageCommand createImageCommand)
    {
        return new ImageEntity
        {
            FileName = Guid.NewGuid().ToString(),
            Image = createImageCommand.Image,
            QuestionId = createImageCommand.QuestionId
        };
    }
}