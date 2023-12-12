using DotNetOverflow.Identity.DAL.Database.Interfaces;

namespace DotNetOverflow.Identity.DAL.Database.Repository;

public class IdentityUnitOfWork(IAppUserRepository appUserRepository,
        AppDbContext accountDbContext)
    : IUnitOfWork
{
    private IAppUserRepository? _appUserRepository = appUserRepository;

    public IAppUserRepository AppUserRepository
    {
        get
        {
            if (_appUserRepository is null)
                _appUserRepository = new AppUserRepository(accountDbContext);
            return _appUserRepository;
        }
    }
}