using Microsoft.AspNetCore.Identity;
using SGCM.Application.Contracts;
using SGCM.Application.Features.Auth.Dtos;
using SGCM.Infrastructure.DbContexts;
using SGCM.Shared.Domain.Entities;
using SGCM.Shared.Result;

namespace SGCM.Application.Features.Auth.Commands;

public sealed class RegisterUserHandler : ICommandHandler<RegisterUserCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly AppDbContext _dbContext;

    public RegisterUserHandler(
        UserManager<ApplicationUser> userManager,
        IEmailService emailService,
        AppDbContext dbContext)
    {
        _userManager = userManager;
        _emailService = emailService;
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(RegisterUserCommand command, CancellationToken cancellationToken = default)
    {
        var existingUser = await _userManager.FindByEmailAsync(command.Email);
        if (existingUser is not null)
            return Result.Failure(Error.Conflict("Auth.EmailTaken", "Email is already registered."));

        var user = new ApplicationUser
        {
            UserName = command.Email,
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName
        };

        var createResult = await _userManager.CreateAsync(user, command.Password);
        if (!createResult.Succeeded)
        {
            var error = createResult.Errors.First();
            return Result.Failure(Error.Validation(error.Code, error.Description));
        }

        await _userManager.AddToRoleAsync(user, command.UserType.ToString());

        if (command.UserType == UserType.Doctor)
            _dbContext.Doctors.Add(Doctor.Create(user.Id, command.FirstName, command.LastName));
        else
            _dbContext.Patients.Add(Patient.Create(user.Id, command.FirstName, command.LastName));

        await _dbContext.SaveChangesAsync(cancellationToken);

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = Uri.EscapeDataString(token);
        var confirmationLink = $"https://localhost:5001/api/auth/confirm-email?userId={user.Id}&token={encodedToken}";

        await _emailService.SendConfirmationEmailAsync(user.Email!, confirmationLink, cancellationToken);

        return Result.Success();
    }
}