using MassTransit;
using VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.EmployeeTaskConsumers
{
    public class EmployeeTaskStatusUpdatedConsumer : IConsumer<EmployeeTaskStatusUpdatedIntegrationEvent>
    {
        private readonly EmployeeTasksService _employeeTasksService;

        public EmployeeTaskStatusUpdatedConsumer(EmployeeTasksService employeeTasksService)
        {
            _employeeTasksService = employeeTasksService;
        }

        public async Task Consume(ConsumeContext<EmployeeTaskStatusUpdatedIntegrationEvent> context)
        {
            await _employeeTasksService.UpdateStatusAsync(context.Message.Id, context.Message.Status);
        }
    }
}