using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Infrastructure.EF.ReadServices;
using VirtualOffice.Infrastructure.EF.Repositories;
using VirtualOffice.Infrastructure.MongoDb.Services;
using VirtualOffice.Infrastructure.RabbitMQ;
using VirtualOffice.Infrastructure.RabbitMQ.Consumers;

namespace VirtualOffice.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // ReadDbServices
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

            // WriteDbRepositories
            services.AddScoped<ICalendarEventRepository, CalendarEventRepository>();
            services.AddScoped<IEmployeeTaskRepository, EmployeeTaskRepository>();
            services.AddScoped<IMeetingRepository, MeetingRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IPrivateChatRoomRepository, PrivateChatRoomRepository>();
            services.AddScoped<IPublicChatRoomRepository, PublicChatRoomRepository>();
            services.AddScoped<IPublicDocumentRepository, PublicDocumentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // ReadServices
            services.AddScoped<ICalendarEventReadService, CalendarEventReadService>();
            services.AddScoped<IEmployeeTaskReadService, EmployeeTaskReadService>();
            services.AddScoped<IMeetingReadService, MeetingReadService>();
            services.AddScoped<INoteReadService, NoteReadService>();
            services.AddScoped<IOrganizationReadService, OrganizationReadService>();
            services.AddScoped<IPrivateChatRoomReadService, PrivateChatRoomReadService>();
            services.AddScoped<IPublicChatRoomReadService, PublicChatRoomReadService>();
            services.AddScoped<IPublicDocumentReadService, PublicDocumentReadService>();
            services.AddScoped<IUserReadService, UserReadService>();

            // fetch data from appsettings.json
            services.Configure<RabbitMQSettings>(
                configuration.GetSection("RabbitMQ"));

            services.AddSingleton(s
            => s.GetRequiredService<IOptions<RabbitMQSettings>>().Value);


            services.AddMassTransit(c =>
            {
                c.SetKebabCaseEndpointNameFormatter();

                c.AddConsumer<CalendarEventCreatedEventConsumer>();

                c.UsingRabbitMq((context, configurator) =>
                {

                    RabbitMQSettings settings = context.GetRequiredService<RabbitMQSettings>();
                    configurator.Host(new Uri(settings.Host), h =>
                    {
                        h.Username(settings.Username);
                        h.Password(settings.Password);
                    });
                    configurator.ReceiveEndpoint("calendar-event-created", e =>
                    {
                        e.ConfigureConsumer<CalendarEventCreatedEventConsumer>(context);
                        e.Bind("calendar-events", x => x.RoutingKey = "CalendarEventCreated");
                    });
                });
            });
            services.AddTransient<IEventBus, EventBus>();

            return services;
        }

        public static IApplicationBuilder UseServicesCollection(this IApplicationBuilder app)
        {
            return app;
        }
    }
}