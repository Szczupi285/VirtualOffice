using MassTransit;
using VirtualOffice.Application.IntegrationEvents.MeetingIntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.MeetingConsumers
{
    public class MeetingEmployeesAddedConsumer : IConsumer<MeetingEmployeesAddedIntegrationEvent>
    {
        private readonly MeetingsService _services;

        public MeetingEmployeesAddedConsumer(MeetingsService services)
        {
            _services = services;
        }

        public async Task Consume(ConsumeContext<MeetingEmployeesAddedIntegrationEvent> context)
        {
            await _services.AddAssignedEmployeesAsync(context.Message.Id, context.Message.AssignedEmployees);
        }
    }
}