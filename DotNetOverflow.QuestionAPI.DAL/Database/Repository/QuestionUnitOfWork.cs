using DotNetOverflow.Identity.DAL.Database;
using DotNetOverflow.QuestionAPI.DAL.Database.Interfaces;

namespace DotNetOverflow.QuestionAPI.DAL.Database.Repository;

public class QuestionUnitOfWork(IQuestionRepository? questionRepository,
        AppDbContext appDbContext)
    : IQuestionUnitOfWork
{
    public IQuestionRepository QuestionRepository
    {
        get
        {
            if (questionRepository is null)
                questionRepository = new QuestionRepository(appDbContext);
            return questionRepository;
        }
    }
}