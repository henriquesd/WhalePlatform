using FluentValidation.Results;
using MediatR;
using WP.Core.Mediator;
using WP.Customer.API.Application.Commands;
using WP.Customer.API.Application.Events;
using WP.Customer.API.Data;
using WP.Customer.API.Repositories;
using WP.Customer.API.Services;
using WP.WebAPI.Core.User;

namespace WP.Customer.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<IRequestHandler<RegisterCustomerCommand, ValidationResult>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<AddAddressCommand, ValidationResult>, CustomerCommandHandler>();

            services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<CustomersContext>();

            services.AddHostedService<RegisterCustomerIntegrationHandler>();
        }
    }
}
