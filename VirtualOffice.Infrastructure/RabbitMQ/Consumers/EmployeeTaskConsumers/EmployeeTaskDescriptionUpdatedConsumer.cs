using MassTransit;
using VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.EmployeeTaskConsumers
{
    public class EmployeeTaskDescriptionUpdatedConsumer : IConsumer<EmployeeTaskDescriptionUpdatedIntegrationEvent>
    {
        private readonly EmployeeTasksService _empTasksService;

        public EmployeeTaskDescriptionUpdatedConsumer(EmployeeTasksService empTasksService)
        {
            _empTasksService = empTasksService;
        }

        public async Task Consume(ConsumeContext<EmployeeTaskDescriptionUpdatedIntegrationEvent> context)
        {
            await _empTasksService.UpdateDescriptionAsync(context.Message.Id, context.Message.Description);
        }
    }
}