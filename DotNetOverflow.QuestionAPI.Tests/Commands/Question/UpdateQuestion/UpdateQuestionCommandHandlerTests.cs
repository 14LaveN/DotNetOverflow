using AutoFixture;
using DotNetOverflow.Core.Enum.Question;
using DotNetOverflow.Core.Enum.StatusCodes;
using DotNetOverflow.Identity.DAL.Database;
using DotNetOverflow.QuestionAPI.Commands.Question.UpdateQuestion;
using DotNetOverflow.QuestionAPI.DAL.Database.Interfaces;
using DotNetOverflow.QuestionAPI.DAL.Database.Repository;
using DotNetOverflow.RabbitMq.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace DotNetOverflow.QuestionAPI.Tests.Commands.Question.UpdateQuestion;

public class UpdateQuestionCommandHandlerTests
{
    private readonly Mock<IRabbitMqService> _rabbitMqServiceMock;
    private readonly IValidator<UpdateQuestionCommand> _validatorMock;
    private readonly Mock<ILogger<UpdateQuestionCommandHandler>> _loggerMock;
    private readonly IQuestionUnitOfWork _questionUnitOfWork;
    private readonly UpdateQuestionCommandHandler _commandHandler;

    public UpdateQuestionCommandHandlerTests()
    {
        _rabbitMqServiceMock = new Mock<IRabbitMqService>();
        _validatorMock = new UpdateQuestionCommandValidator();
        _loggerMock = new Mock<ILogger<UpdateQuestionCommandHandler>>();
        _questionUnitOfWork = new QuestionUnitOfWork(new QuestionRepository(new AppDbContext()), new AppDbContext());
        _commandHandler = new UpdateQuestionCommandHandler(
            _rabbitMqServiceMock.Object,
            _validatorMock,
            _loggerMock.Object,
            _questionUnitOfWork
        );
    }

    [Fact]
    public async Task Handle_WithValidCommand_ReturnsSuccessResponse()
    {
        // Arrange
        var command = new UpdateQuestionCommand
        {
            Id = Guid.Parse("cf9d0e02-62bd-4801-977f-66a4b634473e"),
            Title = "Test Question",
            AuthorId = 1,
            Body = "This is a test question",
            QuestionType = QuestionTypes.Programming,
            Tag = "Test Tag"
        };
        _rabbitMqServiceMock.Setup(x => x.SendMessage(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        // Act
        var response = await _commandHandler.Handle(command);

        // Assert
        Assert.Equal(StatusCode.Ok, response.StatusCode);
        Assert.Equal("Question Updated", response.Description);
        Assert.NotNull(response.Data);
        Assert.Equal(command.Title, response.Data.Title);
        // More assertions for other properties if needed
    }

    [Fact]
    public async Task Handle_WithInvalidCommand_ReturnsValidationErrorResponse()
    {
        // Arrange
        var command = new UpdateQuestionCommand
        {
            Id = Guid.Parse("cf9d0e02-62bd-4801-977f-66a4b634473e"),
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