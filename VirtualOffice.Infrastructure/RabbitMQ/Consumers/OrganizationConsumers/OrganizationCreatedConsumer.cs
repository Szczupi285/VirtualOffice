using MassTransit;
using VirtualOffice.Application.IntegrationEvents.OrganizationIntegrationEvent;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.OrganizationConsumers
{
    public class OrganizationCreatedConsumer : IConsumer<OrganizationCreatedIntegrationEvent>
    {
        private readonly OrganizationsService _organizationService;

        public OrganizationCreatedConsumer(OrganizationsService organizationService)
        {
            _organizationService = organizationService;
        }

        public async Task Consume(ConsumeContext<OrganizationCreatedIntegrationEvent> context)
        {
            await _organizationService.CreateAsync(context.Message);
        }
    }
}