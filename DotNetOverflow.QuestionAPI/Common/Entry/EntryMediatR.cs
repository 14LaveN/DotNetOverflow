using DotNetOverflow.QuestionAPI.Commands.Question.CreateQuestion;
using DotNetOverflow.QuestionAPI.Commands.Question.DeleteQuestion;
using DotNetOverflow.QuestionAPI.Commands.Question.UpdateQuestion;
using DotNetOverflow.QuestionAPI.Queries.Question.GetQuestionDetails;

namespace DotNetOverflow.QuestionAPI.Common.Entry;

public static class EntryMediatr
{public static IServiceCollection AddMediatrExtension(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblies(typeof(CreateQuestionCommand).Assembly,
                typeof(CreateQuestionCommandHandler).Assembly);
            
            x.RegisterServicesFromAssemblies(typeof(UpdateQuestionCommand).Assembly,
                typeof(UpdateQuestionCommandHandler).Assembly);
            
            x.RegisterServicesFromAssemblies(typeof(DeleteQuestionCommand).Assembly,
                typeof(DeleteQuestionCommandHandler).Assembly);
            
            x.RegisterServicesFromAssemblies(typeof(GetQuestionDetailsQuery).Assembly,
                typeof(GetQuestionDetailsQueryHandler).Assembly);
        });
        
        return services;
    }
    
}