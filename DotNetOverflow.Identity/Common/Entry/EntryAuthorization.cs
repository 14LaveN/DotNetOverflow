using System.Text;
using System.Text.Json;
using DotNetOverflow.Core.Entity.Account;
using DotNetOverflow.Identity.DAL.Database;
using DotNetOverflow.Identity.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace DotNetOverflow.Identity.Common.Entry;

public static class EntryAuthorization
{
    public static IServiceCollection AddAuthorizationExtension(this IServiceCollection services,
        IConfiguration configuration)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }
        
        services.AddScoped<UserManager<AppUser>>();
        
        services.AddHttpContextAccessor();
        
        services.AddIdentity<AppUser, IdentityRole<long>>(options =>
            {
                options.User.RequireUniqueEmail = false;
            })
            .AddEntityFrameworkStores<AppDbContext>();
        services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:ValidIssuer"],
                    ValidAudiences = new List<string>(){"https://localhost:7124/", "https://localhost:7269/", "https://localhost:7093/", configuration["Jwt:ValidAudience"]}, // OR "https://localhost:7117/", "https://localhost:7093/"
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                
                        if (string.IsNullOrEmpty(context.Error))
                            context.Error = "invalid_token";
                        if (string.IsNullOrEmpty(context.ErrorDescription))
                            context.ErrorDescription = "This request requires a valid JWT access token to be provided";
                
                        if (context.AuthenticateFailure == null ||
                            context.AuthenticateFailure.GetType() != typeof(SecurityTokenExpiredException))
                            return context.Response.WriteAsync(JsonSerializer.Serialize(new
                            {
                                error = context.Error,
                                error_description = context.ErrorDescription
                            }));
                        var authenticationException = context.AuthenticateFailure as SecurityTokenExpiredException;
                        context.Response.Headers.Add("x-token-expired", authenticationException?.Expires.ToString("o"));
                        context.ErrorDescription =
                            $"The token expired on {authenticationException?.Expires:o}";
                
                        return context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            error = context.Error,
                            error_description = context.ErrorDescription
                        }));
                    }
                };
            });
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        services.AddSingleton<IAuthorizationHandler, RequireScopeHandler>();

        services.AddAuthorization();
        
        return services;
    }
}