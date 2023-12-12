namespace DotNetOverflow.Identity.DAL.Database.Interfaces;

public interface IUnitOfWork
{
    IAppUserRepository AppUserRepository { get; }
}