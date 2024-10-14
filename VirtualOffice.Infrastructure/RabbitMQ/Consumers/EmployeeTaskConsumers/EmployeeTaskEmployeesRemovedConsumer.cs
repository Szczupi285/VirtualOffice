using MassTransit;
using VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.EmployeeTaskConsumers
{
    public class EmployeeTaskEmployeesRemovedConsumer : IConsumer<EmployeeTaskEmployeesRemovedIntegrationEvent>
    {
        private readonly EmployeeTasksService _employeeTasksService;

        public EmployeeTaskEmployeesRemovedConsumer(EmployeeTasksService employeeTasksService)
        {
            _employeeTasksService = employeeTasksService;
        }

        public async Task Consume(ConsumeContext<EmployeeTaskEmployeesRemovedIntegrationEvent> context)
        {
            await _employeeTasksService.RemoveAssignedEmployeesAsync(context.Message.Id, context.Message.AssignedEmployees);
        }
    }
}