using System.Security.Authentication;
using System.Text.Json;
using DotNetOverflow.Core.Entity.Account;
using DotNetOverflow.Core.Enum.StatusCodes;
using DotNetOverflow.Core.Exception;
using DotNetOverflow.Email.Services;
using DotNetOverflow.Identity.Commands.Login;
using DotNetOverflow.Identity.DAL.Database.Interfaces;
using DotNetOverflow.Identity.Models.Identity;
using DotNetOverflow.RabbitMq.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DotNetOverflow.Identity.Commands.Register;

public class RegisterCommandHandler(ILogger<RegisterCommandHandler> logger,
        IValidator<RegisterCommand> validator,
        IUnitOfWork unitOfWork,
        UserManager<AppUser> userManager,
        IMediator mediator,
        IRabbitMqService rabbitMqService,
        IEmailService emailService)
    : IRequestHandler<RegisterCommand, LoginResponse<AppUser>>
{
    public async Task<LoginResponse<AppUser>> Handle(RegisterCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation($"Request for login an account - {request.UserName} {request.Lastname}");

            var errors = await validator.ValidateAsync(request);

            if (errors.Errors.Count is not 0)
            {
                logger.LogWarning($"You have errors - {errors.Errors}");
                throw new ValidationException($"You have erros - {errors.Errors}");
            }
            
            var user = await unitOfWork.AppUserRepository
                .GetByName(request.UserName);

            if (user is not null)
            {
                logger.LogWarning("User with the same name already taken");
                throw new NotFoundException(nameof(user), "User with the same name");
            }

            user = request;
            
            var result = await userManager.CreateAsync(user, request.RetypePassword);
            
            LoginResponse<AppUser> loginResponse = new LoginResponse<AppUser>
            {
                Description = "",
                StatusCode = StatusCode.TaskIsHasAlready
            };

            if (result.Succeeded)
            {
                loginResponse = await mediator.Send(new LoginCommand()
                {
                    UserName = request.UserName,
                    Password = request.RetypePassword
                }, cancellationToken);

                //TODO if (user.Email is not null && user.UserName is not null)
                //TODO     await emailService.SendEmail(user.Email,
                //TODO         user.UserName,
                //TODO         "You authorized to DotNetOverflow");

                logger.LogInformation($"User authorized - {user.UserName} {DateTime.UtcNow}");
                
                var jsonData = JsonSerializer.Serialize(user);
                await rabbitMqService.SendMessage(jsonData, "Users");
            }
            return new LoginResponse<AppUser>
            {
                Description = "Register account",
                StatusCode = StatusCode.Ok,
                Data = user,
                AccessToken = loginResponse.AccessToken, 
                RefreshToken = loginResponse.RefreshToken,
                RefreshTokenExpireAt = loginResponse.RefreshTokenExpireAt
            };
        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[RegisterCommandHandler]: {exception.Message}");
            throw new AuthenticationException(exception.Message);
        }
    }
}