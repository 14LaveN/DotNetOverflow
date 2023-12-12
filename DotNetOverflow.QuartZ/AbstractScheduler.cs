using DotNetOverflow.QuartZ.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;

namespace DotNetOverflow.QuartZ;

public class AbstractScheduler<T> 
    where T: IJob
{
    public static async void Start(IServiceCollection serviceProvider)
    {
        IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            //scheduler.JobFactory = serviceProvider.GetService<QuartzJobFactory>();
        await scheduler.Start();

        IJobDetail jobDetail = JobBuilder.Create<DbTask>().Build();
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity($"{typeof(T)}Trigger", "default")
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(120)
                .RepeatForever())
            .Build();

        await scheduler.ScheduleJob(jobDetail, trigger);
    }
}