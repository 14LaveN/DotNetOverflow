using DotNetOverflow.Identity.DAL.Database;
using DotNetOverflow.QuestionAPI.DAL.Database;
using DotNetOverflow.QuestionAPI.DAL.Database.Interfaces;
using DotNetOverflow.QuestionAPI.DAL.Database.Repository;
using FluentAssertions;

namespace DotNetOverflow.QuestionAPI.Tests.Repository;

public class QuestionRepositoryTests
{
    private readonly IQuestionRepository _questionRepository =
        new QuestionRepository(new AppDbContext(), new QuestionDbContext());

    [Fact]
    public async Task GetQuestionsBySameAuthorId_Handle_WithCommandSuccess()
    {
        var response = await _questionRepository.GetQuestionsBySameAuthorId(1);

        response.Should().NotBeNull();
    }
}