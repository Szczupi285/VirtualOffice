using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;
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
            services.AddReadServices();
            services.AddWriteDbRepositories();
            services.AddReadDbServices();
            services.ConfigureMassTransit(configuration);
            services.AddBackgroudJobs();
            return services;
        }

        private static IServiceCollection AddReadServices(this IServiceCollection services)
        {
            services.AddTransient<ICalendarEventReadService, CalendarEventReadService>();
            services.AddTransient<IEmployeeTaskReadService, EmployeeTaskReadService>();
            services.AddTransient<IMeetingReadService, MeetingReadService>();
            services.AddTransient<INoteReadService, NoteReadService>();
            services.AddTransient<IOrganizationReadService, OrganizationReadService>();
            services.AddTransient<IPrivateChatRoomReadService, PrivateChatRoomReadService>();
            services.AddTransient<IPublicChatRoomReadService, PublicChatRoomReadService>();
            services.AddTransient<IPublicDocumentReadService, PublicDocumentReadService>();
            services.AddTransient<IUserReadService, UserReadService>();

            return services;
        }

        private static IServiceCollection AddWriteDbRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICalendarEventRepository, CalendarEventRepository>();
            services.AddScoped<IEmployeeTaskRepository, EmployeeTaskRepository>();
            services.AddScoped<IMeetingRepository, MeetingRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IPrivateChatRoomRepository, PrivateChatRoomRepository>();
            services.AddScoped<IPublicChatRoomRepository, PublicChatRoomRepository>();
            services.AddScoped<IPublicDocumentRepository, PublicDocumentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOutboxMessageRepository, OutboxMessageRepository>();
            return services;
        }

        private static IServiceCollection AddReadDbServices(this IServiceCollection services)
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

            return services;
        }

        private static IServiceCollection ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
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

        private static IServiceCollection AddBackgroudJobs(this IServiceCollection services)
        {
            services.AddQuartz(cfg =>
            {
                var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
                cfg.AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(trigger =>
                    trigger.ForJob(jobKey)
                    .WithSimpleSchedule(schedule =>
                    schedule.WithIntervalInSeconds(5)
                    .RepeatForever()));
            });
            services.AddQuartzHostedService();
            return services;
        }

        public static IApplicationBuilder UseServicesCollection(this IApplicationBuilder app)
        {
            return app;
        }
    }
}