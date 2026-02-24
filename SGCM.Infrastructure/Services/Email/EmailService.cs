using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SGCM.Application.Contracts;

namespace SGCM.Infrastructure.Services.Email;

public sealed class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendConfirmationEmailAsync(
        string toEmail,
        string confirmationLink,
        CancellationToken cancellationToken = default)
    {
        var smtpSettings = _configuration.GetSection("Smtp");

        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(smtpSettings["From"]));
        message.To.Add(MailboxAddress.Parse(toEmail));
        message.Subject = "Confirm your SGCM account";

        message.Body = new TextPart("html")
        {
            Text = $"""
                    <h2>Welcome to SGCM</h2>
                    <p>Please confirm your email address by clicking the link below.</p>
                    <p>This link will expire in 24 hours.</p>
                    <a href="{confirmationLink}" 
                       style="background-color:#4F46E5;color:white;padding:12px 24px;
                              text-decoration:none;border-radius:4px;">
                        Confirm Email
                    </a>
                    <p>If you did not create an account, you can ignore this email.</p>
                    """
        };

        using var smtp = new SmtpClient();

        await smtp.ConnectAsync(
            smtpSettings["Host"],
            int.Parse(smtpSettings["Port"]!),
            SecureSocketOptions.StartTls,
            cancellationToken);

        await smtp.AuthenticateAsync(
            smtpSettings["Username"],
            smtpSettings["Password"],
            cancellationToken);

        await smtp.SendAsync(message, cancellationToken);
        await smtp.DisconnectAsync(true, cancellationToken);
    }
}