﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtualOffice.Application.Interceptors;
using VirtualOffice.Infrastructure.Identity;

namespace VirtualOffice.Infrastructure.EF
{
    public static class Extensions
    {
        public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WriteDbContext>(
                (sp, options) =>
                {
                    var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();
                    options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
                    options.AddInterceptors(interceptor);
                });


            //Note: for some reason Microsoft.AspNetCore.Identity.UI is required to be installed in order to use AddIdentity in this class.
            services.AddIdentity<AppIdentityUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<WriteDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}