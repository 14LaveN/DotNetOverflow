using System.Text.Json;
using System.Text.Json.Serialization;
using DotNetOverflow.Core.Entity.Image;
using DotNetOverflow.Core.Entity.Question;
using DotNetOverflow.Core.Enum.StatusCodes;
using DotNetOverflow.Core.Responses;
using DotNetOverflow.ImageAPI.DAL.Database.Interfaces;
using DotNetOverflow.RabbitMq.Interfaces;
using FluentValidation;
using MediatR;

namespace DotNetOverflow.ImageAPI.Commands.Image.CreateImage;

public sealed class CreateImageCommandHandler(IRabbitMqService rabbitMqService,
        IValidator<CreateImageCommand> validator,
        ILogger<CreateImageCommandHandler> logger,
        IImageUnitOfWork imageUnitOfWork)
    : IRequestHandler<CreateImageCommand, IBaseResponse<ImageEntity>>
{
    public async Task<IBaseResponse<ImageEntity>> Handle(CreateImageCommand request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            logger.LogInformation($"Request for create an image to question - {request.QuestionId} {DateTime.Now}");
            
            var errors = await validator.ValidateAsync(request, cancellationToken);
    
            if (errors.Errors.Count is not 0)
            {
                throw new ValidationException($"You have errors - {errors.Errors}");
            }

            ImageEntity image = request;
            
            await imageUnitOfWork.ImageRepository.CreateImage(image);
            
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve, // Добавьте эту строку
                MaxDepth = 120 // Вы можете увеличить максимальную глубину, если необходимо
            };
            
            var jsonProduct = JsonSerializer.Serialize(image, options);
            await rabbitMqService.SendMessage(jsonProduct, "Images");
        
            logger.LogInformation($"Image created to - {image.QuestionId} {DateTime.Now}");
            
            return new BaseResponse<ImageEntity>
            {
                Description = "Question created",
                StatusCode = StatusCode.Ok,
                Data = image
            };
        }
        catch (Exception exception)
        {
            logger.LogError(exception.Message, $"[CreateImageCommandHandler]: {exception.Message}");
            return new BaseResponse<ImageEntity>
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}