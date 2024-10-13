using MassTransit;
using VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.EmployeeTaskConsumers
{
    public class EmployeeTaskEmployeesAddedConsumer : IConsumer<EmployeeTaskEmployeesAddedIntegrationEvent>
    {
        private readonly EmployeeTasksService _employeeTasksService;

        public EmployeeTaskEmployeesAddedConsumer(EmployeeTasksService employeeTaskService)
        {
            _employeeTasksService = employeeTaskService;
        }

        public async Task Consume(ConsumeContext<EmployeeTaskEmployeesAddedIntegrationEvent> context)
        {
            await _employeeTasksService.AddAssignedEmployees(context.Message.Id, context.Message.AssignedEmployees);
        }
    }
}