using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using VirtualOffice.Application.Strategies.ScheduleItemDescriptionStrategies;
using VirtualOffice.Application.Strategies.ScheduleItemEmployeesStrategies.Add;
using VirtualOffice.Application.Strategies.ScheduleItemTitleStrategies;

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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // Even thought the strategy factory is stateless, which means we could reigster it as singleton,
            // it consumes scoped IOutboxMessageRepository which is not allowed
            // because it could hold onto this repository beyond its lifetime
            services.AddScoped<ScheduleItemTitleSettedStrategyFactory>();
            services.AddScoped<ScheduleItemDescriptionSettedStrategyFactory>();
            services.AddScoped<ScheduleItemEmployeesAddedStrategyFactory>();

            return services;
        }

        public static IApplicationBuilder UseServicesCollection(this IApplicationBuilder app)
        {
            return app;
        }
    }
}