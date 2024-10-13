using MassTransit;
using VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.EmployeeTaskConsumers
{
    public class EmployeeTaskDeletedConsumer : IConsumer<EmployeeTaskDeletedIntegrationEvent>
    {
        private readonly EmployeeTasksService _employeeTasksService;

        public EmployeeTaskDeletedConsumer(EmployeeTasksService employeeTasksService)
        {
            _employeeTasksService = employeeTasksService;
        }

        public async Task Consume(ConsumeContext<EmployeeTaskDeletedIntegrationEvent> context)
        {
            await _employeeTasksService.RemoveAsync(context.Message.Id.ToString());
        }
    }
}