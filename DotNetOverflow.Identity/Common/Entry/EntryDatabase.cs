using DotNetOverflow.Core.Settings;
using DotNetOverflow.Identity.DAL;
using DotNetOverflow.Identity.DAL.Database;
using Microsoft.EntityFrameworkCore;

namespace DotNetOverflow.Identity.Common.Entry;

public static class EntryDatabase
{
    public static IServiceCollection AddDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddIdentityDatabase(configuration);
        
        services.Configure<MongoSettings>(
            configuration.GetSection("MongoConnection"));
        
        var config = configuration.GetConnectionString("Db");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(config).LogTo(Console.WriteLine));
        
        return services;
    }
}