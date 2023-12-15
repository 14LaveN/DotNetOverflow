using DotNetOverflow.Identity.DAL.Database;
using DotNetOverflow.QuestionAPI.DAL.Database.Interfaces;

namespace DotNetOverflow.QuestionAPI.DAL.Database.Repository;

public class QuestionUnitOfWork(IQuestionRepository? questionRepository,
        AppDbContext appDbContext,
        QuestionDbContext questionDbContext)
    : IQuestionUnitOfWork
{
    public IQuestionRepository QuestionRepository
    {
        get
        {
            if (questionRepository is null)
                questionRepository 
                    = new QuestionRepository(appDbContext, questionDbContext);
            return questionRepository;
        }
    }

    public async Task SaveChangesQuestion(CancellationToken cancellationToken = default)
    {
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}