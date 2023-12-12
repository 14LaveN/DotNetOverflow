using DotNetOverflow.Core.Entity.Account;
using DotNetOverflow.Identity.DAL.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotNetOverflow.Identity.DAL.Database.Repository;

public class AppUserRepository(AppDbContext appDbContext)
    : IAppUserRepository
{
    public async Task SaveChanges()
    {
        await appDbContext.SaveChangesAsync();
    }

    public async Task<AppUser> GetById(long id)
    {
        await appDbContext.Database.BeginTransactionAsync();
        try
        {
            var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            await appDbContext.Database.CommitTransactionAsync();

            return user!;
        }
        catch (Exception)
        {
            await appDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<AppUser> GetByName(string name)
    {
        await appDbContext.Database.BeginTransactionAsync();
        try
        {
            var user = await appDbContext.Users
                .FirstOrDefaultAsync(x => x.UserName == name);
            await appDbContext.Database.CommitTransactionAsync();

            return user!;
        }
        catch (Exception)
        {
            await appDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<List<AppUser>> GetAll()
    {
        await appDbContext.Database.BeginTransactionAsync();
        try
        {
            var users = await appDbContext.Users.ToListAsync();
            await appDbContext.Database.CommitTransactionAsync();

            return users!;
        }
        catch (Exception)
        {
            await appDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }
}