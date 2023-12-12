using DotNetOverflow.Email;
using DotNetOverflow.Identity.Commands.Login;
using DotNetOverflow.Identity.Commands.Register;
using DotNetOverflow.Identity.Common.Entry;
using DotNetOverflow.Identity.Configurations;
using DotNetOverflow.Identity.DAL;
using DotNetOverflow.Identity.Extensions;
using DotNetOverflow.Identity.Middlewares;
using DotNetOverflow.QuartZ;
using DotNetOverflow.QuartZ.Jobs;
using DotNetOverflow.RabbitMq;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Prometheus;
using Prometheus.Client.AspNetCore;
using Prometheus.Client.HttpRequestDurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddRabbitMq("Users");

builder.Configuration
    .AddJsonFile("appsetting.json")
    .Build();

builder.Services.AddEmailService(builder.Configuration);

builder.Services.AddQuartzExtensions();

builder.Services.AddHealthChecksEntry();

builder.Services.AddSwachbackleService()
    .AddValidators();

builder.Services.AddMediatR(x =>
{
    x.RegisterServicesFromAssemblies(typeof(LoginCommand).Assembly,
        typeof(LoginCommandHandler).Assembly);
    
    x.RegisterServicesFromAssemblies(typeof(RegisterCommand).Assembly,
        typeof(RegisterCommandHandler).Assembly);
});

builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddHelpers();

builder.Services.AddAuthorizationExtension(builder.Configuration);

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddDefaultPolicy(corsPolicyBuilder =>
    corsPolicyBuilder.WithOrigins("https://localhost:44460", "http://localhost:44460", "http://localhost:44460/")
        .AllowAnyHeader()
        .AllowAnyMethod()));

builder.Services.AddLoggingExtension(builder.Host);

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

app.Run();