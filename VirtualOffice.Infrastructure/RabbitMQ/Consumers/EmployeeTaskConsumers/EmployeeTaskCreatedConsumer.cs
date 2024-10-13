using MassTransit;
using VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.EmployeeTaskConsumers
{
    public class EmployeeTaskCreatedConsumer : IConsumer<EmployeeTaskCreatedIntegrationEvent>
    {
        private readonly EmployeeTasksService _employeeTasksService;

        public EmployeeTaskCreatedConsumer(EmployeeTasksService employeeTaskService)
        {
            _employeeTasksService = employeeTaskService;
        }

        public async Task Consume(ConsumeContext<EmployeeTaskCreatedIntegrationEvent> context)
        {
            await _employeeTasksService.CreateAsync(context.Message);
        }
    }
}