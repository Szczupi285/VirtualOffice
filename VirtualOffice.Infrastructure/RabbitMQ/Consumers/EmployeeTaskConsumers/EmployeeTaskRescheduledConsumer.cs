using MassTransit;
using VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.EmployeeTaskConsumers
{
    public class EmployeeTaskRescheduledConsumer : IConsumer<EmployeeTaskRescheduledIntegrationEvent>
    {
        private readonly EmployeeTasksService _employeeTaskService;

        public EmployeeTaskRescheduledConsumer(EmployeeTasksService employeeTaskService)
        {
            _employeeTaskService = employeeTaskService;
        }

        public async Task Consume(ConsumeContext<EmployeeTaskRescheduledIntegrationEvent> context)
        {
            await _employeeTaskService.UpdateScheduleAsync(context.Message.Id, context.Message.StartDate, context.Message.EndDate);
        }
    }
}