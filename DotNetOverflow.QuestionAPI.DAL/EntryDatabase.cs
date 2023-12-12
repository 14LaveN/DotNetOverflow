using DotNetOverflow.Identity.DAL.Database;
using DotNetOverflow.Identity.DAL.Database.Interfaces;
using DotNetOverflow.Identity.DAL.Database.Repository;
using DotNetOverflow.QuestionAPI.DAL.Database.Interfaces;
using DotNetOverflow.QuestionAPI.DAL.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetOverflow.QuestionAPI.DAL;

public static class EntryDatabase
{
    public static IServiceCollection AddQuestionDatabase(this IServiceCollection services,
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

        services.AddScoped<IQuestionUnitOfWork, QuestionUnitOfWork>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        
        return services;
    }
}