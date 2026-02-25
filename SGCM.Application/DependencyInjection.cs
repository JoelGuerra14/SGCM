using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SGCM.Application.Contracts;
using SGCM.Application.Features.Auth.Commands;
using SGCM.Application.Features.Patients.Commands;
using SGCM.Application.Features.Patients.Dtos;
using SGCM.Application.Features.Patients.Queries;
using SGCM.Application.Features.Patients.Validators;
using SGCM.Shared.Result;

namespace SGCM.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAuthHandlers(services);
        AddPatientHandlers(services);
    }

    private static void AddAuthHandlers(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<RegisterUserCommand, Result>, RegisterUserHandler>();
        services.AddScoped<ICommandHandler<ConfirmEmailCommand, Result>, ConfirmEmailHandler>();
    }

    private static void AddPatientHandlers(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreatePatientCommand>, CreatePatientCommandValidator>();
        services.AddScoped<ICommandHandler<CreatePatientCommand, Result>, CreatePatientCommandHandler>();

        services.AddScoped<IValidator<UpdatePatientCommand>, UpdatePatientCommandValidator>();
        services.AddScoped<ICommandHandler<UpdatePatientCommand, Result>, UpdatePatientCommandHandler>();

        services.AddScoped<IQueryHandler<GetPatientByIdQuery, Result<PatientResponseDto>>, GetPatientByIdQueryHandler>();
    }
}