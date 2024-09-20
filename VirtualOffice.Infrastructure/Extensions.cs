﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtualOffice.Infrastructure.MongoDb.Services;

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
            services.AddSingleton<PrivateDocumentsService>();
            services.AddSingleton<PublicChatRoomsService>();
            services.AddSingleton<PublicDocumentsService>();

            return services;
        }

        public static IApplicationBuilder UseServicesCollection(this IApplicationBuilder app)
        {
            return app;
        }
    }
}