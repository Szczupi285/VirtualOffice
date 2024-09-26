using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using VirtualOffice.Infrastructure.MongoDb.Services;
using VirtualOffice.Infrastructure.RabbitMQ;

namespace VirtualOffice.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<EmployeesService>();
            services.AddSingleton<CalendarEventsService>();
            services.AddSingleton<EmployeeTasksService>();
            services.AddSingleton<MeetingsService>();
            services.AddSingleton<NotesService>();
            services.AddSingleton<OfficesService>();
            services.AddSingleton<OrganizationsService>();
            services.AddSingleton<PrivateChatRoomsService>();
            services.AddSingleton<PublicChatRoomsService>();
            services.AddSingleton<PublicDocumentsService>();

            // fetch data from appsettings.json
            services.Configure<RabbitMQSettings>(
                configuration.GetSection("RabbitMQ"));

            services.AddSingleton(s
            => s.GetRequiredService<IOptions<RabbitMQSettings>>().Value);

            services.AddMassTransit(c =>
            {
                c.SetKebabCaseEndpointNameFormatter();

                c.UsingRabbitMq((context, configurator) =>
                {
                    RabbitMQSettings settings = context.GetRequiredService<RabbitMQSettings>();
                    configurator.Host(new Uri(settings.Host), h =>
                    {
                        h.Username(settings.Username);
                        h.Password(settings.Password);
                    });
                });
            });

            return services;
        }

        public static IApplicationBuilder UseServicesCollection(this IApplicationBuilder app)
        {
            return app;
        }
    }
}