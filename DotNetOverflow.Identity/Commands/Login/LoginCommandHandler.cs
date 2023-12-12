using System.Security.Authentication;
using System.Text.Json;
using DotNetOverflow.Core.Entity.Account;
using DotNetOverflow.Core.Enum.StatusCodes;
using DotNetOverflow.Core.Exception;
using DotNetOverflow.Identity.DAL.Database.Interfaces;
using DotNetOverflow.Identity.Extensions;
using DotNetOverflow.Identity.Models.Identity;
using DotNetOverflow.RabbitMq.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using AppUser = DotNetOverflow.Core.Entity.Account.AppUser;

namespace DotNetOverflow.Identity.Commands.Login;

public class LoginCommandHandler(ILogger<LoginCommandHandler> logger,
        IValidator<LoginCommand> validator,
        IUnitOfWork unitOfWork,
        UserManager<AppUser> userManager,
        IOptions<JwtOptions> jwtOptions,
        IRabbitMqService rabbitMqService)
    : IRequestHandler<LoginCommand, LoginResponse<AppUser>>
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public async Task<LoginResponse<AppUser>> Handle(LoginCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation($"Request for login an account - {request.UserName}");

            var errors = await validator.ValidateAsync(request, cancellationToken);

            if (errors.Errors.Count is not 0)
            {
                throw new ValidationException($"You have errors - {errors.Errors}");
            }
            
            var user = await unitOfWork.AppUserRepository
                .GetByName(request.UserName);

            if (user is null)
            {
                logger.LogWarning("User with the same name not found");
                throw new NotFoundException(nameof(user), "User with the same name");
            }

            if (!await userManager.CheckPasswordAsync(user, request.Password))
            {
                logger.LogWarning("The password does not meet the assessment criteria");
                throw new AuthenticationException();
            }
        
            var (refreshToken, refreshTokenExpireAt) = user.GenerateRefreshToken(_jwtOptions);
            user.RefreshToken = refreshToken;
            await unitOfWork.AppUserRepository.SaveChanges();
            
            logger.LogInformation($"User logged in - {user.UserName} {DateTime.UtcNow}");
                
            var jsonData = JsonSerializer.Serialize(user);
            await rabbitMqService.SendMessage(jsonData, "Users");
        
            return new LoginResponse<AppUser>
            {
                Description = "Login account",
                StatusCode = StatusCode.Ok,
                Data = user,
                AccessToken = user.GenerateAccessToken(_jwtOptions), 
                RefreshToken = refreshToken,
                RefreshTokenExpireAt = refreshTokenExpireAt
            };
        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[LoginCommandHandler]: {exception.Message}");
            throw new AuthenticationException(exception.Message);
        }
    }
}