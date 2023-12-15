using System.Text.Json;
using System.Text.Json.Serialization;
using DotNetOverflow.Core.Entity.Question;
using DotNetOverflow.Core.Enum.StatusCodes;
using DotNetOverflow.Core.Responses;
using DotNetOverflow.QuestionAPI.DAL.Database.Interfaces;
using DotNetOverflow.RabbitMq.Interfaces;
using FluentValidation;
using MediatR;
using MongoDB.Bson;

namespace DotNetOverflow.QuestionAPI.Commands.Question.CreateQuestion;

public class CreateQuestionCommandHandler(IRabbitMqService rabbitMqService,
        IValidator<CreateQuestionCommand> validator,
        ILogger<CreateQuestionCommandHandler> logger,
        IQuestionUnitOfWork questionUnitOfWork)
    : IRequestHandler<CreateQuestionCommand, IBaseResponse<QuestionEntity>>
{
    public async Task<IBaseResponse<QuestionEntity>> Handle(CreateQuestionCommand request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            logger.LogInformation($"Request for create a question by title - {request.Title} {DateTime.Now}");
            
            var errors = await validator.ValidateAsync(request, cancellationToken);
    
            if (errors.Errors.Count is not 0)
            {
                throw new ValidationException($"You have errors - {errors.Errors}");
            }

            QuestionEntity question = request;
            await questionUnitOfWork.QuestionRepository.CreateQuestion(question);
            
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve, // Добавьте эту строку
                MaxDepth = 120 // Вы можете увеличить максимальную глубину, если необходимо
            };
            
            var jsonProduct = JsonSerializer.Serialize(question, options);
            await rabbitMqService.SendMessage(jsonProduct, "Questions");
        
            logger.LogInformation($"Question created - {question.Title} {DateTime.Now}");
            
            return new BaseResponse<QuestionEntity>
            {
                Description = "Question created",
                StatusCode = StatusCode.Ok,
                Data = question
            };
        }
        catch (Exception exception)
            //when(exception.StatusCode == StatusCode.InternalServerError)
        {
            logger.LogError(exception.Message, $"[CreateQuestionCommandHandler]: {exception.Message}");
            return new BaseResponse<QuestionEntity>
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}