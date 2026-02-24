using SGCM.Application.Contracts;
using SGCM.Application.Features.Auth.Dtos;
using SGCM.Shared.Result;

namespace SGCM.Application.Features.Auth.Commands;

public sealed record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    UserType UserType
) : ICommand<Result>;