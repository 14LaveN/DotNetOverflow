using System.Text.Json;
using System.Text.Json.Serialization;
using DotNetOverflow.Core.Entity.Question;
using DotNetOverflow.Core.Enum.StatusCodes;
using DotNetOverflow.Core.Exception;
using DotNetOverflow.Core.Responses;
using DotNetOverflow.QuestionAPI.DAL.Database.Interfaces;
using DotNetOverflow.RabbitMq.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace DotNetOverflow.QuestionAPI.Commands.Question.UpdateQuestion;

public class UpdateQuestionCommandHandler(IRabbitMqService rabbitMqService,
        IValidator<UpdateQuestionCommand> validator,
        ILogger<UpdateQuestionCommandHandler> logger,
        IQuestionUnitOfWork questionUnitOfWork)
    : IRequestHandler<UpdateQuestionCommand, IBaseResponse<QuestionEntity>>
{
    public async Task<IBaseResponse<QuestionEntity>> Handle(UpdateQuestionCommand request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            logger.LogInformation($"Request for update a question by id - {request.Id} {DateTime.Now}");
            
            var errors = await validator.ValidateAsync(request, cancellationToken);
    
            if (errors.Errors.Count is not 0)
            {
                throw new ValidationException($"You have errors - {errors.Errors}");
            }

            var question = await questionUnitOfWork.QuestionRepository.GetQuestionById(request.Id);
            
            if (question!.Title.IsNullOrEmpty())
            {
                logger.LogWarning($"Question with the same id - {request.Id} not found");

                throw new NotFoundException(nameof(UpdateQuestionCommand), "with the same id");
            }
            
            await questionUnitOfWork.QuestionRepository.UpdateQuestion(question!);
            
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve, // Добавьте эту строку
                MaxDepth = 120 // Вы можете увеличить максимальную глубину, если необходимо
            };
            
            var jsonProduct = JsonSerializer.Serialize(question, options);
            await rabbitMqService.SendMessage(jsonProduct, "Questions");
        
            logger.LogInformation($"Question updated - {question.Title} {DateTime.Now}");
            
            return new BaseResponse<QuestionEntity>
            {
                Description = "Question updated",
                StatusCode = StatusCode.Ok,
                Data = question
            };
        }
        catch (Exception exception)
        {
            logger.LogError(exception.Message, $"[UpdateQuestionCommandHandler]: {exception.Message}");
            return new BaseResponse<QuestionEntity>
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}