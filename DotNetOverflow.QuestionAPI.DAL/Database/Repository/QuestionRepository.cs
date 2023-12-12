using DotNetOverflow.Core.Entity.Question;
using DotNetOverflow.Identity.DAL.Database;
using DotNetOverflow.QuestionAPI.DAL.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotNetOverflow.QuestionAPI.DAL.Database.Repository;

public class QuestionRepository(AppDbContext appDbContext)
    : IQuestionRepository
{
    public async Task CreateQuestion(QuestionEntity questionEntity)
    {
        await appDbContext.Database.BeginTransactionAsync();
        try
        {
            await appDbContext.Questions.AddAsync(questionEntity);
            await appDbContext.SaveChangesAsync();
            
            await appDbContext.Database.CommitTransactionAsync();
        }
        catch (Exception)
        {
            await appDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<IQueryable<QuestionEntity>> GetAllQuestions()
    {
        await appDbContext.Database.BeginTransactionAsync();
        try
        {
            var questions = appDbContext.Questions
                .Include(x=>x.AppUser);
            await appDbContext.Database.CommitTransactionAsync();

            return questions;
        }
        catch (Exception)
        {
            await appDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task DeleteQuestion(QuestionEntity questionEntity)
    {
        await appDbContext.Database.BeginTransactionAsync();
        try
        {
            appDbContext.Questions.Remove(questionEntity);
            await appDbContext.SaveChangesAsync();
            
            await appDbContext.Database.CommitTransactionAsync();
        }
        catch (Exception)
        {
            await appDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<QuestionEntity> UpdateQuestion(QuestionEntity questionEntity)
    {
        await appDbContext.Database.BeginTransactionAsync();
        try
        { 
            appDbContext.Questions.Update(questionEntity);
            await appDbContext.SaveChangesAsync();
            
            await appDbContext.Database.CommitTransactionAsync();

            return questionEntity;
        }
        catch (Exception)
        {
            await appDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<IQueryable<QuestionEntity>?> GetQuestionsByName(string name)
    {
        await appDbContext.Database.BeginTransactionAsync();
        try
        {
            var questions = appDbContext.Questions
                .Include(x => x.AppUser)
                .Where(x => x.AppUser!.UserName == name);
            
            await appDbContext.SaveChangesAsync();
            
            await appDbContext.Database.CommitTransactionAsync();

            return questions;
        }
        catch (Exception)
        {
            await appDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<QuestionEntity?> GetQuestionById(Guid id)
    {
        await appDbContext.Database.BeginTransactionAsync();
        try
        {
            var question = await appDbContext.Questions
                .Include(x => x.AppUser)
                .FirstOrDefaultAsync(x=>x.Id == id);
            
            await appDbContext.SaveChangesAsync();
            
            await appDbContext.Database.CommitTransactionAsync();

            return question;
        }
        catch (Exception)
        {
            await appDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }
}