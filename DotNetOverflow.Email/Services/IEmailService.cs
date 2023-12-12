namespace DotNetOverflow.Email.Services;

public interface IEmailService
{
    Task SendEmail(string email, string subject, string message);
}