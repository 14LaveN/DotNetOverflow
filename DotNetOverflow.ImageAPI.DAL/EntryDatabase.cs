using DotNetOverflow.Identity.DAL.Database;
using DotNetOverflow.ImageAPI.DAL.Database.Interfaces;
using DotNetOverflow.ImageAPI.DAL.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetOverflow.ImageAPI.DAL;

public static class EntryDatabase
{
    public static IServiceCollection AddImageDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }
        
        var config = configuration.GetConnectionString("Db");
        
        services.AddDbContext<AppDbContext>(o => 
            o.UseNpgsql(config)
                .LogTo(Console.WriteLine)
                .EnableServiceProviderCaching());

        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IImageUnitOfWork, ImageUnitOfWork>();
        
        return services;
    }
}