using DotNetOverflow.Core.Entity.Account;

namespace DotNetOverflow.Identity.DAL.Database.Interfaces;

public interface IAppUserRepository
{
    Task SaveChanges();

    Task<AppUser> GetById(long id);

    Task<AppUser> GetByName(string name);

    Task<List<AppUser>> GetAll();
}