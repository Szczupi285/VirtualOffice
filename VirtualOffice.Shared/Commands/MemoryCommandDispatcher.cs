using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Commands;

namespace VirtualOffice.Shared.Commands
{
    public class MemoryCommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceScopeFactory _serviceProvider;

        public MemoryCommandDispatcher(IServiceScopeFactory serviceProvider)
         =>   _serviceProvider = serviceProvider;

        public async Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : class, ICommand
        {
            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            await handler.HandleAsync(command, cancellationToken);
        }
    }
}
