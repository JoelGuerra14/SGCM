using Microsoft.Extensions.DependencyInjection;
using SGCM.Application.Contracts;
using SGCM.Application.Features.Auth.Commands;
using SGCM.Shared.Result;

namespace SGCM.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAuthHandlers(services);
    }
    
    private static void AddAuthHandlers(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<RegisterUserCommand, Result>, RegisterUserHandler>();
        services.AddScoped<ICommandHandler<ConfirmEmailCommand, Result>, ConfirmEmailHandler>();
    }
}