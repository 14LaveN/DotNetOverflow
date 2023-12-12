using System.ComponentModel.DataAnnotations;
using DotNetOverflow.Core.Entity.Question;
using DotNetOverflow.Core.Enum.Question;
using DotNetOverflow.Core.Enum.StatusCodes;
using DotNetOverflow.Identity.DAL.Database;
using DotNetOverflow.QuestionAPI.Commands.Question.CreateQuestion;
using DotNetOverflow.QuestionAPI.DAL.Database.Interfaces;
using DotNetOverflow.QuestionAPI.DAL.Database.Repository;
using DotNetOverflow.RabbitMq.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;

namespace DotNetOverflow.QuestionAPI.Tests.Commands.Question.CreateQuestion;

public class CreateQuestionCommandHandlerTests
{
    private readonly Mock<IRabbitMqService> _rabbitMqServiceMock;
    private readonly IValidator<CreateQuestionCommand> _validatorMock;
    private readonly Mock<ILogger<CreateQuestionCommandHandler>> _loggerMock;
    private readonly IQuestionUnitOfWork questionUnitOfWork;
    private readonly CreateQuestionCommandHandler _commandHandler;

    public CreateQuestionCommandHandlerTests()
    {
        _rabbitMqServiceMock = new Mock<IRabbitMqService>();
        _validatorMock = new CreateQuestionCommandValidator();
        _loggerMock = new Mock<ILogger<CreateQuestionCommandHandler>>();
        questionUnitOfWork = new QuestionUnitOfWork(new QuestionRepository(new AppDbContext()), new AppDbContext());
        _commandHandler = new CreateQuestionCommandHandler(
            _rabbitMqServiceMock.Object,
            _validatorMock,
            _loggerMock.Object,
            questionUnitOfWork
        );
    }

    [Fact]
    public async Task Handle_WithValidCommand_ReturnsSuccessResponse()
    {
        // Arrange
        var command = new CreateQuestionCommand
        {
            Title = "Test Question",
            Body = "This is a test question",
            QuestionType = QuestionTypes.Programming,
            AuthorId = 1,
            Tag = "Test Tag"
        };
        _rabbitMqServiceMock.Setup(x => x.SendMessage(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        // Act
        var response = await _commandHandler.Handle(command);

        // Assert
        Assert.Equal(StatusCode.Ok, response.StatusCode);
        Assert.Equal("Question created", response.Description);
        Assert.NotNull(response.Data);
        Assert.Equal(command.Title, response.Data.Title);
        // More assertions for other properties if needed
    }

    [Fact]
    public async Task Handle_WithInvalidCommand_ReturnsValidationErrorResponse()
    {
        // Arrange
        var command = new CreateQuestionCommand
        {
            Title = "Test Question",
            Body = "This is a test question",
            QuestionType = QuestionTypes.Programming,
            AuthorId = 1,
            Tag = "Test Tag"
        };

        // Act
        var response = await _commandHandler.Handle(command);

        // Assert
        Assert.Equal(StatusCode.InternalServerError, response.StatusCode);
        Assert.Contains("Title is required", response.Description);
        Assert.Null(response.Data);
    }
}