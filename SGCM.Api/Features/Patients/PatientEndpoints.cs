using Microsoft.AspNetCore.Mvc;
using SGCM.Application.Contracts;
using SGCM.Application.Features.Patients.Commands;
using SGCM.Application.Features.Patients.Dtos;
using SGCM.Shared.Result;
using System.Security.Claims;

namespace SGCM.Api.Features.Patients
{
    public static class PatientEndpoints
    {
        public static void MapPatientEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/patients").WithTags("Patients").RequireAuthorization();

            group.MapPost("/", async (
                [FromBody] CreatePatientRequest request,
                ClaimsPrincipal user,
                ICommandHandler<CreatePatientCommand, Result> handler,
                CancellationToken cancellationToken) =>
            {
                
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                    return Results.Unauthorized();

                var command = new CreatePatientCommand(
                    userId,
                    request.Phone,
                    request.Address,
                    request.BloodType,
                    request.Allergies,
                    request.MedicalConditions,
                    request.EmergencyContactName,
                    request.EmergencyContactPhone,
                    request.InsurerId,
                    request.PolicyNumber
                );

                var result = await handler.Handle(command, cancellationToken);

                return result.IsSuccess
                    ? Results.Ok(new { message = "Patient profile completed succesfully." })
                    : result.Error.Type switch
                    {
                        ErrorType.Validation => Results.BadRequest(new { result.Error.Code, result.Error.Message }),
                        ErrorType.NotFound => Results.NotFound(new { result.Error.Code, result.Error.Message }),
                        _ => Results.Problem("An unexpected error occurred.")
                    };
            });
        }
    }
}
