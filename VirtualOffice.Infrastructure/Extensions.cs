using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;
using System.Reflection;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Infrastructure.EF.ReadServices;
using VirtualOffice.Infrastructure.EF.Repositories;
using VirtualOffice.Infrastructure.MongoDb.Services;
using VirtualOffice.Infrastructure.RabbitMQ;
using VirtualOffice.Infrastructure.RabbitMQ.Consumers;
using VirtualOffice.Infrastructure.RabbitMQ.Consumers.CalendarEventConsumers;
using VirtualOffice.Infrastructure.RabbitMQ.Consumers.EmployeeTaskConsumers;
using VirtualOffice.Infrastructure.RabbitMQ.Consumers.MeetingConsumers;

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
                // refactor

                // register all consumers in current assembly
                c.AddConsumers(Assembly.GetExecutingAssembly());

                c.UsingRabbitMq((context, configurator) =>
                {
                    RabbitMQSettings settings = context.GetRequiredService<RabbitMQSettings>();
                    configurator.Host(new Uri(settings.Host), h =>
                    {
                        h.Username(settings.Username);
                        h.Password(settings.Password);
                    });
                    // to do: write the configs more nicely
                    configurator.ReceiveEndpoint("calendar-event-created", e =>
                    {
                        e.ConfigureConsumer<CalendarEventCreatedEventConsumer>(context);
                        e.Bind("calendar-events", x => x.RoutingKey = "CalendarEventCreated");
                    });
                    configurator.ReceiveEndpoint("calendar-event-updated", e =>
                    {
                        e.ConfigureConsumer<CalendarEventTitleUpdatedConsumer>(context);
                        e.ConfigureConsumer<CalendarEventDescriptionUpdatedConsumer>(context);
                        e.ConfigureConsumer<CalendarEventRescheduledConsumer>(context);
                        e.ConfigureConsumer<CalendarEventEmployeesAddedConsumer>(context);
                        e.ConfigureConsumer<CalendarEventEmployeesRemovedConsumer>(context);

                        e.Bind("calendar-events", x => x.RoutingKey = "CalendarEventUpdated");
                    });
                    configurator.ReceiveEndpoint("calendar-event-deleted", e =>
                    {
                        e.ConfigureConsumer<CalendarEventDeletedConsumer>(context);
                        e.Bind("calendar-events", x => x.RoutingKey = "CalendarEventDeleted");
                    });

                    configurator.ReceiveEndpoint("employee-task-created", e =>
                    {
                        e.ConfigureConsumer<EmployeeTaskCreatedConsumer>(context);
                        e.Bind("employee-tasks", x => x.RoutingKey = "EmployeeTaskCreated");
                    });
                    configurator.ReceiveEndpoint("employee-task-deleted", e =>
                    {
                        e.ConfigureConsumer<EmployeeTaskDeletedConsumer>(context);
                        e.Bind("employee-tasks", x => x.RoutingKey = "EmployeeTaskDeleted");
                    });
                    configurator.ReceiveEndpoint("employee-task-updated", e =>
                    {
                        e.ConfigureConsumer<EmployeeTaskEmployeesAddedConsumer>(context);
                        e.ConfigureConsumer<EmployeeTaskEmployeesRemovedConsumer>(context);
                        e.ConfigureConsumer<EmployeeTaskTitleUpdatedConsumer>(context);
                        e.ConfigureConsumer<EmployeeTaskDescriptionUpdatedConsumer>(context);
                        e.ConfigureConsumer<EmployeeTaskRescheduledConsumer>(context);
                        e.ConfigureConsumer<EmployeeTaskStatusUpdatedConsumer>(context);
                        e.ConfigureConsumer<EmployeeTaskPriorityUpdatedConsumer>(context);

                        e.Bind("employee-tasks", x => x.RoutingKey = "EmployeeTaskUpdated");
                    });
                    configurator.ReceiveEndpoint("meeting-created", e =>
                    {
                        e.ConfigureConsumer<MeetingCreatedConsumer>(context);

                        e.Bind("meetings", x => x.RoutingKey = "MeetingCreated");
                    });
                    configurator.ReceiveEndpoint("meeting-deleted", e =>
                    {
                        e.ConfigureConsumer<MeetingDeletedConsumer>(context);

                        e.Bind("meetings", x => x.RoutingKey = "MeetingDeleted");
                    });
                    configurator.ReceiveEndpoint("meeting-updated", e =>
                    {
                        e.ConfigureConsumer<MeetingTitleUpdatedConsumer>(context);
                        e.ConfigureConsumer<MeetingDescriptionUpdatedConsumer>(context);
                        e.ConfigureConsumer<MeetingRescheduledConsumer>(context);
                        e.ConfigureConsumer<MeetingEmployeesAddedConsumer>(context);

                        e.Bind("meetings", x => x.RoutingKey = "MeetingUpdated");
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
                    schedule.WithIntervalInSeconds(30)
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