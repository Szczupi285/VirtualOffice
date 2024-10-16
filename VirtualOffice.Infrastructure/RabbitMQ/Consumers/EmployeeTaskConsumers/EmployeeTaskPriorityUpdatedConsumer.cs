using MassTransit;
using VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.EmployeeTaskConsumers
{
    public class EmployeeTaskPriorityUpdatedConsumer : IConsumer<EmployeeTaskPriorityUpdatedIntegrationEvent>
    {
        private readonly EmployeeTasksService _employeeTasksService;

        public EmployeeTaskPriorityUpdatedConsumer(EmployeeTasksService employeeTasksService)
        {
            _employeeTasksService = employeeTasksService;
        }

        public async Task Consume(ConsumeContext<EmployeeTaskPriorityUpdatedIntegrationEvent> context)
        {
            await _employeeTasksService.UpdatePriorityAsync(context.Message.Id, context.Message.Priority);
        }
    }
}