using Microsoft.Extensions.DependencyInjection;
using BuildingBlocks;
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace BankAccount.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly);
        services.AddMessageBroker(configuration, assembly);

        return services;
    }
}
