using DotNetOverflow.Identity.DAL.Database;
using DotNetOverflow.Identity.DAL.Database.Interfaces;
using DotNetOverflow.Identity.DAL.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetOverflow.Identity.DAL;

public static class EntryDatabase
{
    public static IServiceCollection AddIdentityDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }
        
        var config = configuration.GetConnectionString("Db");
        
        services.AddDbContext<AppDbContext>(o => 
            o.UseNpgsql(config, act 
                    =>
                {
                    act.EnableRetryOnFailure(3);
                    act.CommandTimeout(30);
                })
                .LogTo(Console.WriteLine)
                .EnableServiceProviderCaching()
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());
        
        services.AddScoped<IAppUserRepository, AppUserRepository>();
        services.AddScoped<IUnitOfWork, IdentityUnitOfWork>();
        
        return services;
    }
}