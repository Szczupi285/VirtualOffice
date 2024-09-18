using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualOffice.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddServicesCollection(this IServiceCollection services)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
            }
            return services;
        }

        public static IApplicationBuilder UseServicesCollection(this IApplicationBuilder app)
        {
            return app;
        }
    }
}