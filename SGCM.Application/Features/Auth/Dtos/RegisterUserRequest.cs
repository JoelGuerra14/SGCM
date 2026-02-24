namespace SGCM.Application.Features.Auth.Dtos;

public sealed record RegisterUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    UserType UserType
);

public enum UserType
{
    Doctor,
    Patient
}