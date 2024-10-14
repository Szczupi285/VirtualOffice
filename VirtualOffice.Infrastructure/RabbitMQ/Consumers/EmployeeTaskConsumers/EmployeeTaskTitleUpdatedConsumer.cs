using MassTransit;
using VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.EmployeeTaskConsumers
{
    public class EmployeeTaskTitleUpdatedConsumer : IConsumer<EmployeeTaskTitleUpdatedIntegrationEvent>
    {
        private readonly EmployeeTasksService _empTasksService;

        public EmployeeTaskTitleUpdatedConsumer(EmployeeTasksService empTasksService)
        {
            _empTasksService = empTasksService;
        }

        public async Task Consume(ConsumeContext<EmployeeTaskTitleUpdatedIntegrationEvent> context)
        {
            await _empTasksService.UpdateTitleAsync(context.Message.Id, context.Message.Title);
        }
    }
}