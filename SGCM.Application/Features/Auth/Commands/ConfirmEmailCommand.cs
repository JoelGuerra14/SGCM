using SGCM.Application.Contracts;
using SGCM.Shared.Result;

namespace SGCM.Application.Features.Auth.Commands;

public sealed record ConfirmEmailCommand(
    string UserId,
    string Token
) : ICommand<Result>;