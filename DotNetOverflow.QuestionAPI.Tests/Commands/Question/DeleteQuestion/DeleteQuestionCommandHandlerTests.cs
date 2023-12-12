using DotNetOverflow.Core.Entity.Question;
using DotNetOverflow.Core.Enum.Question;
using DotNetOverflow.Core.Enum.StatusCodes;
using DotNetOverflow.Identity.DAL.Database;
using DotNetOverflow.QuestionAPI.Commands.Question.DeleteQuestion;
using DotNetOverflow.QuestionAPI.DAL.Database.Interfaces;
using DotNetOverflow.QuestionAPI.DAL.Database.Repository;
using DotNetOverflow.RabbitMq.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;

namespace DotNetOverflow.QuestionAPI.Tests.Commands.Question.DeleteQuestion;

public class DeleteQuestionCommandHandlerTests
{
    private readonly Mock<IRabbitMqService> _rabbitMqServiceMock;
    private readonly IValidator<DeleteQuestionCommand> _validatorMock;
    private readonly Mock<ILogger<DeleteQuestionCommandHandler>> _loggerMock;
    private readonly IQuestionUnitOfWork _questionUnitOfWorkMock;
    private readonly DeleteQuestionCommandHandler _commandHandler;

    public DeleteQuestionCommandHandlerTests()
    {
        _rabbitMqServiceMock = new Mock<IRabbitMqService>();
        _validatorMock = new DeleteQuestionCommandValidator();
        _loggerMock = new Mock<ILogger<DeleteQuestionCommandHandler>>();
        _questionUnitOfWorkMock = new QuestionUnitOfWork(new QuestionRepository(new AppDbContext()), new AppDbContext());
        _commandHandler = new DeleteQuestionCommandHandler(
            _rabbitMqServiceMock.Object,
            _validatorMock,
            _loggerMock.Object,
            _questionUnitOfWorkMock
        );
    }

    [Fact]
    public async Task Handle_WithValidCommand_ReturnsSuccessResponse()
    {
        // Arrange
        var command = new DeleteQuestionCommand
        {
            Author = "dsfsfs",
            Id = Guid.Parse("b7fe3520-3019-4d3b-b0e3-ce26dedf351f")
        };
        var errors = await _validatorMock.ValidateAsync(command, default);
        _rabbitMqServiceMock.Setup(x => x.SendMessage(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        // Act
        var response = await _commandHandler.Handle(command);

        // Assert
        Assert.Equal(StatusCode.Ok, response.StatusCode);
        Assert.Equal("Question deleted", response.Description);
        Assert.NotNull(response.Data);
        // More assertions for other properties if needed
    }

    [Fact]
    public async Task Handle_WithInvalidCommand_ReturnsValidationErrorResponse()
    {
        // Arrange
        var command = new DeleteQuestionCommand
        {
            Author = "dsfsfs",
            Id = Guid.Parse("b7fe3520-3019-4d3b-b0e3-ce26dedf351f")
        };

        // Act
        var response = await _commandHandler.Handle(command);

        // Assert
        Assert.Equal(StatusCode.InternalServerError, response.StatusCode);
        Assert.Contains("Title is required", response.Description);
        Assert.Null(response.Data);
    }
}