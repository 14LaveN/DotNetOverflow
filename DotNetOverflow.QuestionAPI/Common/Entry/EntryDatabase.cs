using DotNetOverflow.Core.Settings;
using DotNetOverflow.Identity.DAL;
using DotNetOverflow.QuestionAPI.DAL;

namespace DotNetOverflow.QuestionAPI.Common.Entry;

public static class EntryDatabase
{
    public static IServiceCollection AddDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }
        
        services.Configure<MongoSettings>(
            configuration.GetSection("MongoConnection"));

        services.AddQuestionDatabase(configuration);
        services.AddIdentityDatabase(configuration);
        
        return services;
    }
}