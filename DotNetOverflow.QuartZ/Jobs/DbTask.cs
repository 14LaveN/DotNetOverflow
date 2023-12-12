using DotNetOverflow.Identity.DAL.Database;
using Quartz;
using static System.Console;

namespace DotNetOverflow.QuartZ.Jobs;

public class DbTask : IJob
{
    private readonly AppDbContext _appDbContext = new AppDbContext();

    public async Task Execute(IJobExecutionContext context)
    {
        await _appDbContext.SaveChangesAsync();
        WriteLine("SaveChanges");
    }
}