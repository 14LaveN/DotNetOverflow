using DotNetOverflow.Core.Entity.Account;
using DotNetOverflow.Identity.Models.Identity;
using MediatR;

namespace DotNetOverflow.Identity.Commands.Login;

public class LoginCommand
    : IRequest<LoginResponse<AppUser>>
{
    public required string UserName { get; set; } = null!;
    public required string Password { get; set; } = null!;
}