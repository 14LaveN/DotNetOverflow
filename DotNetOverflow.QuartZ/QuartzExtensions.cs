using DotNetOverflow.QuartZ.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Impl;
using Quartz.Spi;

namespace DotNetOverflow.QuartZ;

public static class QuartzExtensions
{
    public static IServiceCollection AddQuartzExtensions(this IServiceCollection services)
    {
        services.AddTransient<IJobFactory, QuartzJobFactory>();
        services.AddSingleton(provider =>
        {
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().Result;
            return scheduler;
        });
        services.AddTransient<AbstractScheduler<DbTask>>();
        return services;
    }
}