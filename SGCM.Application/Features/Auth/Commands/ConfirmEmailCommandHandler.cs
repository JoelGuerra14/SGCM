using Microsoft.AspNetCore.Identity;
using SGCM.Application.Contracts;
using SGCM.Shared.Domain.Entities;
using SGCM.Shared.Result;

namespace SGCM.Application.Features.Auth.Commands;

public sealed class ConfirmEmailHandler : ICommandHandler<ConfirmEmailCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ConfirmEmailHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(ConfirmEmailCommand command, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(command.UserId);
        if (user is null)
            return Result.Failure(Error.NotFound("Auth.UserNotFound", "User not found."));

        var decodedToken = Uri.UnescapeDataString(command.Token);
        var confirmResult = await _userManager.ConfirmEmailAsync(user, decodedToken);

        if (!confirmResult.Succeeded)
        {
            var error = confirmResult.Errors.First();
            return Result.Failure(Error.Validation(error.Code, error.Description));
        }

        return Result.Success();
    }
}