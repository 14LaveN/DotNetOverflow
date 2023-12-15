namespace DotNetOverflow.QuestionAPI.DAL.Database.Interfaces;

public interface IQuestionUnitOfWork
{
    IQuestionRepository QuestionRepository { get; }

    Task SaveChangesQuestion(CancellationToken cancellationToken = default);
}