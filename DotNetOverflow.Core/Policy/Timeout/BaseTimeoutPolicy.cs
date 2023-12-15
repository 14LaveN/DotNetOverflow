using Polly.Timeout;

namespace DotNetOverflow.Core.Policy.Timeout;

public static class BaseTimeoutPolicy
{
    public static readonly AsyncTimeoutPolicy<HttpResponseMessage> PolicyTimeout = Polly.Policy
        .TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10)); 
}