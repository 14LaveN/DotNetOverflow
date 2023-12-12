using DotNetOverflow.Core.Entity.Image;
using DotNetOverflow.Core.Responses;
using MediatR;

namespace DotNetOverflow.ImageAPI.Commands.Image.CreateImages;

public class CreateImagesCommand
    : IRequest<IBaseResponse<List<ImageEntity>>>
{
    public required List<ImageEntity> Images { get; set; }

    public required Guid QuestionId { get; set; }
}