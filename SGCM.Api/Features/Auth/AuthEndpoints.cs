using Microsoft.AspNetCore.Mvc;
using SGCM.Application.Contracts;
using SGCM.Application.Features.Auth.Commands;
using SGCM.Application.Features.Auth.Dtos;
using SGCM.Shared.Result;

namespace SGCM.Api.Features.Auth;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth").WithTags("Auth");

        group.MapPost("/register", async (
            [FromBody] RegisterUserRequest request,
            ICommandHandler<RegisterUserCommand, Result> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new RegisterUserCommand(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password,
                request.UserType);

            var result = await handler.Handle(command, cancellationToken);

            return result.IsSuccess
                ? Results.Ok(new { message = "Registration successful. Please check your email to confirm your account." })
                : result.Error.Type switch
                {
                    ErrorType.Conflict => Results.Conflict(new { result.Error.Code, result.Error.Message }),
                    ErrorType.Validation => Results.BadRequest(new { result.Error.Code, result.Error.Message }),
                    _ => Results.Problem("An unexpected error occurred.")
                };
        });

        group.MapGet("/confirm-email", async (
            [FromQuery] string userId,
            [FromQuery] string token,
            ICommandHandler<ConfirmEmailCommand, Result> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new ConfirmEmailCommand(userId, token);
            var result = await handler.Handle(command, cancellationToken);

            return result.IsSuccess
                ? Results.Redirect("http://localhost:5173/login?confirmed=true")
                : result.Error.Type switch
                {
                    ErrorType.NotFound => Results.NotFound(new { result.Error.Code, result.Error.Message }),
                    ErrorType.Validation => Results.BadRequest(new { result.Error.Code, result.Error.Message }),
                    _ => Results.Problem("An unexpected error occurred.")
                };
        });
    }
}