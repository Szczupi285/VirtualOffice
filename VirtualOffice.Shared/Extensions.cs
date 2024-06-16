using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Commands;
using VirtualOffice.Shared.Commands;

namespace VirtualOffice.Shared
{
    public static class Extensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            services.AddScoped<ICommandDispatcher, MemoryCommandDispatcher>();
            return services;
        }
    }
}
