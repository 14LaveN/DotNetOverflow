namespace DotNetOverflow.QuestionAPI.DAL.Database.Interfaces;

public interface IQuestionUnitOfWork
{
    IQuestionRepository QuestionRepository { get; }
}