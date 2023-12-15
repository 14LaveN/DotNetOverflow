using DotNetOverflow.QuartZ;
using DotNetOverflow.QuartZ.Jobs;
using DotNetOverflow.QuestionAPI.Common.Entry;
using DotNetOverflow.QuestionAPI.Configurations;
using DotNetOverflow.QuestionAPI.Handlers;
using DotNetOverflow.QuestionAPI.Middlewares;
using DotNetOverflow.RabbitMq;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Prometheus;
using Prometheus.Client.AspNetCore;
using Prometheus.Client.HttpRequestDurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddValidators();

builder.Services.AddMediatrExtension();

builder.Services.AddHelpers();

builder.Services.AddHealthChecksEntry();

builder.Services.AddLogs(builder.Host);

builder.Services.AddCachingEntry();

builder.Services.AddAuthorizationEntry();

builder.Services.AddRabbitMq("Questions");

builder.Services.AddQuartzExtensions();

builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddSwachbackleService();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

AbstractScheduler<DbTask>.Start(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerApp();
}

app.UseCors();

app.UseMetricServer();

app.UseHttpMetrics();

app.UsePrometheusServer();

app.UsePrometheusRequestDurations();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
});

app.MapControllers();

app.UseCustomMiddlewares();

app.UseExceptionHandler("/error");

app.Run();