namespace SGCM.Application.Contracts;

public interface IEmailService
{
    Task SendConfirmationEmailAsync(string toEmail, string confirmationLink, CancellationToken cancellationToken = default);
}