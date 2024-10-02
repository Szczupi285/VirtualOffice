using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using VirtualOffice.Application.DomainEventHandlers;
using VirtualOffice.Domain.DomainEvents.CalendarEventEvents;

namespace VirtualOffice.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
            }
            services.AddScoped<INotificationHandler<CalendarEventCreated>, CalendarEventCreatedDomainEventHandler>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

        public static IApplicationBuilder UseServicesCollection(this IApplicationBuilder app)
        {
            return app;
        }
    }
}