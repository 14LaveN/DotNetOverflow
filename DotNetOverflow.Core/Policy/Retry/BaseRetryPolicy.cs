using Polly;
using Polly.Retry;
using Polly.Timeout;

namespace DotNetOverflow.Core.Policy.Retry;

public static class BaseRetryPolicy
{
    public static readonly ResiliencePipeline Policy = new ResiliencePipelineBuilder()
        .AddRetry(new RetryStrategyOptions
        {
            ShouldHandle =
                new PredicateBuilder()
                    .Handle<System.Exception>(),
            MaxRetryAttempts = 3,
            Delay = TimeSpan.FromSeconds(1),
            BackoffType =
                DelayBackoffType.Constant
        })
        .AddTimeout(new TimeoutStrategyOptions
        {
            Timeout = TimeSpan.FromSeconds(10)
        })
        .Build();
}