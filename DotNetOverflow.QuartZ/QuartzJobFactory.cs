using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace DotNetOverflow.QuartZ;

public class QuartzJobFactory : IJobFactory
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public QuartzJobFactory(IServiceScopeFactory serviceScopeFactory)
    {
        this._serviceScopeFactory = serviceScopeFactory;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        
        var job = scope.ServiceProvider.GetService(bundle.JobDetail.JobType) as IJob;
        return job!;
    }

    public void ReturnJob(IJob job)
    {
        var disposable = job as IDisposable;
        disposable?.Dispose();
    }
}