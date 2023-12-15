using DotNetOverflow.Core.Entity.Account;
using DotNetOverflow.Core.Entity.Question;

namespace DotNetOverflow.QuestionAPI.DAL.Database.Interfaces;

public interface IQuestionRepository
{
    Task CreateQuestion(QuestionEntity questionEntity);

    Task<IEnumerable<string>> GetQuestionsBySameAuthorId(long id);

    Task<IQueryable<QuestionEntity>> GetAllQuestions();

    Task DeleteQuestion(QuestionEntity questionEntity);

    Task<QuestionEntity?> GetQuestionByIdNoTracing(Guid id);

    Task<QuestionEntity> UpdateQuestion(QuestionEntity questionEntity);

    Task<IQueryable<QuestionEntity>?> GetQuestionsByName(string name);

    Task<QuestionEntity?> GetQuestionById(Guid id);
}