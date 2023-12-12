using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DotNetOverflow.Identity.DAL.Database.HealthCheck;

public class GeneralDbHealthCheck(AppDbContext articleDbContext) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var conn = articleDbContext.Database;
        {
            try
            {
                await conn.OpenConnectionAsync(cancellationToken);
            }
            catch (DbException ex)
            {
                return new HealthCheckResult(status: context.Registration.FailureStatus, exception: ex);
            }
        }

        return HealthCheckResult.Healthy();
    }
}