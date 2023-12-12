using DotNetOverflow.Core.Entity.Question;

namespace DotNetOverflow.QuestionAPI.DAL.Database.Interfaces;

public interface IQuestionRepository
{
    Task CreateQuestion(QuestionEntity questionEntity);

    Task<IQueryable<QuestionEntity>> GetAllQuestions();

    Task DeleteQuestion(QuestionEntity questionEntity);

    Task<QuestionEntity> UpdateQuestion(QuestionEntity questionEntity);

    Task<IQueryable<QuestionEntity>?> GetQuestionsByName(string name);

    Task<QuestionEntity?> GetQuestionById(Guid id);
}