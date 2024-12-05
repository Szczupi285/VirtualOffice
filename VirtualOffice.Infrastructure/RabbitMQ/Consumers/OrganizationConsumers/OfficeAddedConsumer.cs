using MassTransit;
using VirtualOffice.Application.IntegrationEvents.OrganizationIntegrationEvent;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.OrganizationConsumers
{
    public class OfficeAddedConsumer : IConsumer<OfficeAddedIntegrationEvent>
    {
        private readonly OrganizationsService _organizationService;

        public OfficeAddedConsumer(OrganizationsService organizationService)
        {
            _organizationService = organizationService;
        }

        public async Task Consume(ConsumeContext<OfficeAddedIntegrationEvent> context)
        {

            await _organizationService.AddOfficeAsync(context.Message.OrganizationId, context.Message.Office);
        }
    }
}