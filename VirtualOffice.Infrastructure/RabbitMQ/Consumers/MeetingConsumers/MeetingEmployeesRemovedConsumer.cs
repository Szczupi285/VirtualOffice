using MassTransit;
using VirtualOffice.Application.IntegrationEvents.MeetingIntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.MeetingConsumers
{
    public class MeetingEmployeesRemovedConsumer : IConsumer<MeetingEmployeesRemovedIntegrationEvent>
    {
        private readonly MeetingsService _meetingsService;

        public MeetingEmployeesRemovedConsumer(MeetingsService meetingsService)
        {
            _meetingsService = meetingsService;
        }

        public async Task Consume(ConsumeContext<MeetingEmployeesRemovedIntegrationEvent> context)
        {
            await _meetingsService.RemoveAssignedEmployeesAsync(context.Message.Id, context.Message.AssignedEmployees);
        }
    }
}