using DotNetOverflow.QuestionAPI.Commands.Question.CreateQuestion;
using DotNetOverflow.QuestionAPI.Commands.Question.DeleteQuestion;
using DotNetOverflow.QuestionAPI.Commands.Question.UpdateQuestion;
using DotNetOverflow.QuestionAPI.Queries.Question.GetQuestionDetails;
using FluentValidation;

namespace DotNetOverflow.QuestionAPI.Common.Entry;

public static class EntryValidator
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddScoped<IValidator<CreateQuestionCommand>, CreateQuestionCommandValidator>();
        services.AddScoped<IValidator<DeleteQuestionCommand>, DeleteQuestionCommandValidator>();
        services.AddScoped<IValidator<UpdateQuestionCommand>, UpdateQuestionCommandValidator>();
        services.AddScoped<IValidator<GetQuestionDetailsQuery>, GetQuestionDetailsQueryValidator>();
        
        return services;
    }
}